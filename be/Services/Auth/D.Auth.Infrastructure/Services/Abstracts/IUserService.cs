using D.Auth.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto request);
    }
}
