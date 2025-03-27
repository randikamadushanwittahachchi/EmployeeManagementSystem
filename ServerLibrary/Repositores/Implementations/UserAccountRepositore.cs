using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.Extensions.Options;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Repositores.Implementations
{
    public class UserAccountRepositore : IUserAccount
    {
        private readonly IOptions<JWTSection> _config;
        private readonly IUserAccountService _context;
        private readonly ISystemRoleService _systemRoleContext;
        private readonly IAccountService _dbContext;
        public UserAccountRepositore(IOptions<JWTSection> config, IUserAccountService context,ISystemRoleService systemRoleContext, IAccountService dbContext)
        {
            _context = context;
            _systemRoleContext = systemRoleContext;
            _dbContext = dbContext;
            _config = config;
        }
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "Model is Empty");
            var checkUser = await _context.FindUserByEmail(user.Email!);
            if (checkUser is not null) return new GeneralResponse(false,"User Register Alredy");

            //save in data base
            var appUser = new AppUser
            {
                Email = user.Email,
                FullName = user.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            };

            var appUserNew = await _dbContext.AddToDatabase(appUser);
            if (appUserNew is null) return new GeneralResponse(false, "User Registration is fail");

            var checkAdminSystemRole = await _systemRoleContext.FindByName(Constants.Admin);
            if(checkAdminSystemRole is null)
            {
                var newSystemRole =await _dbContext.AddToDatabase(new SystemRole { Name=Constants.Admin});
                if (newSystemRole is null) return new GeneralResponse(false, "Role Registration is fail");

                var adminUserRole = await _dbContext.AddToDatabase(new UserRole{ UserId=appUserNew.Id,RoleId=newSystemRole.Id});
                if (adminUserRole is null) return new GeneralResponse(false, "Role Registration is fail");
                return new GeneralResponse(true, "Registration is Success");
            }

            var checkUserSystemRole = await _systemRoleContext.FindByName(Constants.User);
            if (checkUserSystemRole is null)
            {
                var newSystemRole = await _dbContext.AddToDatabase(new SystemRole { Name = Constants.User });
                if (newSystemRole is null) return new GeneralResponse(false, "Role Registration is fail");

                var userUserRole = await _dbContext.AddToDatabase(new UserRole { UserId = appUserNew.Id, RoleId = newSystemRole.Id });
                if (userUserRole is null) return new GeneralResponse(false, "Role Registration is fail");
                return new GeneralResponse(true, "Registration is Success");
            }
            else
            {
                var userUserRole = await _dbContext.AddToDatabase(new UserRole { UserId = appUserNew.Id, RoleId = checkUserSystemRole.Id });
                if (userUserRole is null) return new GeneralResponse(false, "Role Registration is fail");
                return new GeneralResponse(true, "Registration is Success");
            }
        }

        public Task<LoginResponse> SinginAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
