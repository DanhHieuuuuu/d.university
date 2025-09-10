## Công nghệ sử dụng
- C# .net 9 ASP .netcore
- SQL server
## Lệnh chạy Migration 
- Cách chạy mgration : 
``` xml
PS E:\download\d.university\be\Services\Core\D.Core.Domain> dotnet ef migrations add checkmgs --context CoreDBContext --startup-project ..\D.Core.API
```
## Lệnh update data base 
- Cách chạy mgration : 
``` xml
PS E:\download\d.university\be\Services\Core\D.Core.Domain> dotnet ef update database checkmgs --context CoreDBContext --startup-project ..\D.Core.API
```