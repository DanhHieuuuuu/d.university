using D.Auth.Infrastructure.Repositories;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure
{
    public class ServiceUnitOfWork
    {
        private IDbContext _dbContext;
        private IHttpContextAccessor _httpContext;

        public ServiceUnitOfWork(IDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        private NsNhanSuRepository _nsNhanSuRepository;
        private RoleRepository _roleRepository;
        private RolePermissionRepository _rolePermissionRepository;
        private UserRoleRepository _userRoleRepository;
        private UserRepository _userRepository;
        public INsNhanSuRepository iNsNhanSuRepository
        {
            get
            {
                if(_nsNhanSuRepository == null)
                {
                    _nsNhanSuRepository = new NsNhanSuRepository(_dbContext, _httpContext);
                }
                return _nsNhanSuRepository;
            }
        }

        public IRoleRepository iRoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_dbContext, _httpContext);
                }
                return _roleRepository;
            }
        }

        public IRolePermissionRepository iRolePermissionRepository
        {
            get
            {
                if (_rolePermissionRepository == null)
                {
                    _rolePermissionRepository = new RolePermissionRepository(_dbContext, _httpContext);
                }
                return _rolePermissionRepository;
            }
        }

        public IUserRoleRepository iUserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null)
                {
                    _userRoleRepository = new UserRoleRepository(_dbContext, _httpContext);
                }
                return _userRoleRepository;
            }
        }

        public IUserRepository iUserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext, _httpContext);
                }
                return _userRepository;
            }
        }
    }
}
