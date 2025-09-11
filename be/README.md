## Công nghệ sử dụng

- C# .NET 9
- SQL Server
- Entity Framework Core (EF Core)

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

---

## Cập nhật Database

### 1. Áp dụng migration vào SQL Server

Chạy lệnh sau để update database theo migration mới nhất:

```powershell
dotnet ef database update --context CoreDBContext --startup-project ..\D.Core.API
```

---
