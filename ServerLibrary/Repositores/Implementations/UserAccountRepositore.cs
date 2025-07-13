using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Authentication;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Contracts;

namespace ServerLibrary.Repositores.Implementations;

public class UserAccountRepositore
{
    private readonly AppDbContext _context;
    private readonly SystemRoleRepository _systemRoleRepository;
    private readonly UserRoleRepository _userRoleRepository;
    private readonly TokenService _tokenService;
    private readonly RefreshTokenInfoRepository _refreshTokenInfoRepository;
    public UserAccountRepositore(AppDbContext context, SystemRoleRepository systemRoleRepository, UserRoleRepository userRoleRepository,TokenService tokenService, RefreshTokenInfoRepository refreshTokenInfoRepository)
    {
        _context = context;
        _systemRoleRepository = systemRoleRepository;
        _userRoleRepository = userRoleRepository;
        _tokenService = tokenService;
        _refreshTokenInfoRepository = refreshTokenInfoRepository;
    }
    public async Task<GeneralResponse> CreateAsync(Register user)
    {
        var errorMessage = Validation.ValidateModel<Register>(user);
        if (errorMessage.Any())
        {
            return new GeneralResponse(false, string.Join(" ", errorMessage));
        }
       
        var checkUser = await GetByEmail(user.Email);
        if (checkUser is not null) return new GeneralResponse(false,"User Register Alredy");

        //save in data base
        var appUser = new AppUser
        {
            Email = user.Email.Trim().ToLower(),
            FullName = user.FullName.Trim(),
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password.Trim()),
        };

        var appUserResponse = await Create(appUser);
        if (!appUserResponse.Flag) return appUserResponse;

        var appUserNew = await GetByEmail(appUser.Email);
        if(appUserNew is null) return new GeneralResponse(false, "User Registration is failed");

        var checkUserSystemRole = await _systemRoleRepository.GetByName(Constants.User);
        if (checkUserSystemRole is null) return new GeneralResponse(false, "Fails to Create System-Role");
        var response = await _userRoleRepository.Create(new UserRole { UserId = appUserNew.Id, RoleId = checkUserSystemRole.Id });
        return response;
    }

    public async Task<LoginResponse> SigninAsync(Login user)
    {
        if (user is null || user.Email is null) return new LoginResponse(false,"Empty user Model");

        var appUser = await GetByEmail(user.Email);
        if (appUser is null) return new LoginResponse(false,"User not found");

        if (!BCrypt.Net.BCrypt.Verify(user.Password, appUser.Password)) return new LoginResponse(false, "Email/Password not valid");

        var userRole = await _userRoleRepository.FindByUserId(appUser.Id);
        if (userRole is null) return new LoginResponse(false, "Role is not found");

        var roleName = await _systemRoleRepository.GetById(userRole.RoleId);
        if (roleName is null) return new LoginResponse(false, "Role is not valid");

        string jwtToken = _tokenService.GetToken(appUser, roleName.Name!);
        if (jwtToken == null) return new LoginResponse(false, "Token is fails");

        string refreshToken = RefreshTokenService.GetToken();

        var refreshTokenInfo = _refreshTokenInfoRepository.Add(new RefreshTokenInfo
        {
            UserId = appUser.Id,
            Token = refreshToken,
        });
        if (refreshTokenInfo is null) return new LoginResponse(false ,"Refresh Token is fail");

        return new LoginResponse(true, "Login is success", jwtToken, refreshToken);
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken)
    {
        if (refreshToken == null) return new LoginResponse(false, "Token is empty");

        var checkToken = await _refreshTokenInfoRepository.FindByToken(refreshToken.Token!);
        if (checkToken == null || checkToken.UserId == null) return new LoginResponse(false,"Invalid Token");

        var appUser = await FindById(checkToken.UserId.Value);
        if (appUser is null) return new LoginResponse(false, "Invalid user");

        var userRole = await _userRoleRepository.FindByUserId(appUser.Id);
        if (userRole is null) return new LoginResponse(false, "Invalid role");

        var role = await _systemRoleRepository.GetById(userRole.RoleId);
        if (role is null) return new LoginResponse(false, "Invalid role");

        string jwtToken = _tokenService.GetToken(appUser,role.Name!);
        if (jwtToken == null) return new LoginResponse(false, "Token is fail");

        string newRefreshToken = RefreshTokenService.GetToken();

        checkToken.Token = newRefreshToken;

        await _refreshTokenInfoRepository.Update(checkToken);

        return new LoginResponse(true,"success",jwtToken,newRefreshToken);
    }

    public async Task<List<AppUser>> GetAll() => await _context.AppUsers.ToListAsync();

    public async Task<AppUser?> GetById(int id) => await FindById(id);

    public async Task<AppUser?> GetByEmail(string email) => await FindByEmail(email);
    public async Task<GeneralResponse>Create(AppUser appUser)
    {
        var item = await FindByEmail(appUser.Email);
        if (item is not null) return Exited();
        var result = _context.AppUsers.Add(appUser);
        await Commit();
        return Success();
    }
    public async Task<GeneralResponse>Update(AppUser appUser)
    {
        var user = await GetById(appUser.Id);
        if (user is null) return NotFound();

        user.Autherize = appUser.Autherize;

        await Commit();
        return Success();
    }
    public async Task<GeneralResponse>DeleteById(int id)
    {
        var appUser = await _context.AppUsers.FindAsync(id);
        if (appUser is null) return NotFound();

        _context.AppUsers.Remove(appUser);
        await Commit();

        return Success();
    }

    private async Task Commit() => await _context.SaveChangesAsync();
    private static GeneralResponse Unsuccess() => new GeneralResponse(false, nameof(AppUser) + ConstantsResponse.Unsuccess);
    private static GeneralResponse Success() => new GeneralResponse(true, ConstantsResponse.Success);
    private static GeneralResponse Exited() => new GeneralResponse(false, nameof(AppUser) + ConstantsResponse.Exit);
    private async Task<AppUser?> FindById(int id) => await _context.AppUsers.FindAsync(id);
    private async Task<AppUser?> FindByEmail(string email) => await _context.AppUsers.FirstOrDefaultAsync(_ => _.Email!.Trim().ToLower() == email!.Trim().ToLower());
    private static GeneralResponse NotFound() => new GeneralResponse(false, ConstantsResponse.NotFound);

}
