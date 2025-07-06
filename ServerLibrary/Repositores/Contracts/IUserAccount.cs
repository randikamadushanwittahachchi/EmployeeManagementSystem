using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositores.Contracts;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAsync(Register register);
    Task<LoginResponse> SigninAsync(Login login);
    Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken);
    Task<List<AppUser>> GetAll();
    Task<AppUser?> GetById(int id);
    Task<AppUser?> GetByEmail(string email);
    Task<GeneralResponse> Create(AppUser appUser);
    Task<GeneralResponse> Update(AppUser appUser);
    Task<GeneralResponse> DeleteById(int id);

}
