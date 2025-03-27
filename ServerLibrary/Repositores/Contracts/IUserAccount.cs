using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositores.Contracts;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAsync(Register register);
    Task<LoginResponse> SinginAsync(Login login);
}
