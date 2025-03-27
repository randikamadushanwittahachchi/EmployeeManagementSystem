using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.Extensions.Options;
using ServerLibrary.Authentication;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;
using ServerLibrary.Services.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class UserAccountRepositore : IUserAccount
{
    private readonly IUserAccountService _context;
    private readonly ISystemRoleService _systemRoleContext;
    private readonly IAccountService _dbContext;
    private readonly IUserRoleService _userRoleContext;
    private readonly TokenService _tokenService;
    private readonly IRefreshTokenInfoService _refreshTokenInfoContext;
    public UserAccountRepositore(IUserAccountService context,ISystemRoleService systemRoleContext,IUserRoleService userRoleContext, IAccountService dbContext,TokenService tokenService, IRefreshTokenInfoService refreshTokenInfoService)
    {
        _context = context;
        _systemRoleContext = systemRoleContext;
        _userRoleContext = userRoleContext;
        _dbContext = dbContext;
        _tokenService = tokenService;
        _refreshTokenInfoContext = refreshTokenInfoService;
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

    public async Task<LoginResponse> SigninAsync(Login user)
    {
        if (user is null) return new LoginResponse(false,"Empty user Model");

        var appUser = await _context.FindUserByEmail(user.Email);
        if (appUser is null) return new LoginResponse(false,"User not found");

        if (!BCrypt.Net.BCrypt.Verify(user.Password, appUser.Password)) return new LoginResponse(false, "Email/Password not valid");

        var userRole = await _userRoleContext.FindByUserId(appUser.Id);
        if (userRole is null) return new LoginResponse(false, "Role is not found");

        var roleName = await _systemRoleContext.FindById(userRole.RoleId);
        if (roleName is null) return new LoginResponse(false, "Role is not valid");

        string jwtToken = _tokenService.GetToken(appUser, roleName.Name!);
        string refreshToken = RefreshTokenService.GetToken();
        if (jwtToken == null) return new LoginResponse(false, "Token is fails");
        return new LoginResponse(true, "Login is success", jwtToken, refreshToken);
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken)
    {
        if (refreshToken == null) return new LoginResponse(false, "Token is empty");

        var checkToken = await _refreshTokenInfoContext.FindByToken(refreshToken.Token!);
        if (checkToken == null) return new LoginResponse(false,"Invalid Token");

        var appUser = await _context.FindUserById(checkToken.UserId!);
        if (appUser is null) return new LoginResponse(false, "Invalid user");

        var userRole = await _userRoleContext.FindByUserId(appUser.Id);
        if (userRole is null) return new LoginResponse(false, "Invalid role");

        var role = await _systemRoleContext.FindById(userRole.RoleId);
        if (role is null) return new LoginResponse(false, "Invalid role");

        string jwtToken = _tokenService.GetToken(appUser,role.Name!);
        if (jwtToken == null) return new LoginResponse(false, "Token is fail");

        string newRefreshToken = RefreshTokenService.GetToken();

        checkToken.Token = newRefreshToken;

        await _refreshTokenInfoContext.Update(checkToken);

        return new LoginResponse(true,"success",jwtToken,newRefreshToken);
    }
}
