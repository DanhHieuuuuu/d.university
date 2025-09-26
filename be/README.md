## Công nghệ sử dụng

- C# .NET 9
- SQL Server
- Entity Framework Core (EF Core)

---

## Hướng dẫn chạy Migration

### 1. Di chuyển vào thư mục `D.Core.Domain`

```powershell
cd Services/Core/D.Core.Domain
```

### 2. Tạo migration mới

Dùng lệnh sau, thay `<migration_name>` bằng tên bạn muốn (ví dụ: `InitDatabase`):

```powershell
dotnet ef migrations add <migration_name> --context CoreDBContext --startup-project ..\D.Core.API
```

=> Lệnh này sẽ sinh ra file migration trong thư mục **Migrations** của `D.Core.Domain`.

### 3. Xóa migration

Dùng lệnh sau để xóa migration (chỉ xóa được khi chưa update database):

```powershell
dotnet ef migrations remove --context CoreDBContext --startup-project ..\D.Core.API
```

---

## Cập nhật Database

### 1. Áp dụng migration vào SQL Server

Chạy lệnh sau để update database theo migration mới nhất:

```powershell
dotnet ef database update --context CoreDBContext --startup-project ..\D.Core.API
```

---

## Cài đặt redis (ubuntu)

```xml
sudo apt update
sudo apt install redis-server -y
sudo systemctl enable redis-server
sudo systemctl start redis-server
```

- Check : redis-cli ping => pong => sucess
- kiểm tra port (dòng port): sudo nano /etc/redis/redis.conf

---

## Flow từ entity => controller

1. Tạo `entity` extends `EntityBase` (khai báo `DbSet`)

2. Tạo repository để lấy dữ liệu từ db

ví dụ:

```xml
using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Auth.Infrastructure.Repositories
{
    public class NsNhanSuRepository : RepositoryBase<NsNhanSu> INsNhanSuRepository
    {
        public NsNhanSuRepository(IDbContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }
    }

    public interface INsNhanSuRepository : IRepositoryBase<NsNhanSu> { }
}

```

`RepositoryBase` và `IRepositoryBase` phải là của namespace `D.InfrastructureBase.Repository`.

`IDbContext` phải là của namespace `D.InfrastructureBase.Database`.

Inject chúng vào trong file `DependencyInjection` ngay dưới.

3. Tạo file `service` và `iService` để viết logic nghiệp vụ

Ví dụ:

```xml
using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
```

Khai báo các biến như trên và inject vào `DependencyInjection`

4. Khai báo dto cho MediatR

Ví dụ:

```xml
    public class NsNhanSuRequestDto : FilterBaseDto, IQuery<PageResultDto<NsNhanSuResponseDto>>
    {
        [FromQuery(Name = "cccd")]
        public string? Cccd { get; set; }
    }
```

Request nào thì phải implement Response đó, có thể khai báo kiểu `IQuery` hoặc `ICommand` tùy theo logic, sử dụng namespace `D.DomainBase.Common`.

<div style="text-align: center;">
  <img src="overview_cqrs.png" alt="CQRS Design Pattern in Microservices Architectures" />
</div>

5. Xử lý truy vấn MediatR gọi đến

Ví dụ:

```xml
using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class FindPagingNsNhanSu
        : IQueryHandler<NsNhanSuRequestDto, PageResultDto<NsNhanSuResponseDto>>
    {
        public INsNhanSuService _nsNhanSuService;

        public FindPagingNsNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<PageResultDto<NsNhanSuResponseDto>> Handle(
            NsNhanSuRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.FindPagingNsNhanSu(request);
        }
    }
}

```

Sử dụng `IQueryHandler` hoặc `ICommandHandler` của namespace `D.ApplicationBase` để service biết resquest, response là gì

6. Viết controller

Ví dụ:

```xml
        public async Task<ResponseAPI> GetListNhanSu(NsNhanSuRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
```

Chỉ cần truyền Request vào thì mediator sẽ biết response là gì => mỗi api đều cần 1 request riêng biệt
