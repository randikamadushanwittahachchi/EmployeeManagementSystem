using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ServerLibrary.Helpers;
using ServerLibrary.Repositores.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations;

public class ManageUserService
{
    private readonly UserAccountRepositore _userAccountRepository;
    private readonly UserRoleRepository _userRoleRepository;
    private readonly SystemRoleRepository _systemRoleRepository;
    public ManageUserService(UserAccountRepositore userAccountRepository, UserRoleRepository userRoleRepository, SystemRoleRepository systemRoleRepository)
    {
        _userAccountRepository = userAccountRepository;
        _userRoleRepository = userRoleRepository;
        _systemRoleRepository = systemRoleRepository;
    }
    public async Task<List<ManageUser>> GetAll()
    {
        var users = await _userAccountRepository.GetAll();
        var systemRoles = await _systemRoleRepository.GetAll();
        var userRoles = await _userRoleRepository.GetAll();

        var manageUsers = new List<ManageUser>();

        if(!users.Any() || !userRoles.Any()) return new List<ManageUser>();

        foreach(var user in users)
        {
            var userRole = userRoles.FirstOrDefault(_ => _.UserId == user.Id);
            if (userRole is null) continue;
            var systemRole = systemRoles.FirstOrDefault(_ => _.Id == userRole.RoleId);
            if (systemRole is null) continue;
            manageUsers.Add(new ManageUser
            {
                UserId = user.Id,
                UserName = user.FullName,
                Email = user.Email,
                Role = systemRole.Name,
                Autherize = user.Autherize,
            });
        }
        return manageUsers;
    }

    public async Task<ResultResponse<ManageUser>>GetById(int id)
    {
        var manageUser = new ManageUser();

        var user = await _userAccountRepository.GetById(id);
        if (user is null) return ResultResponse<ManageUser>.Failure("App User" + ConstantsResponse.NotFound);

        var userRole = await _userRoleRepository.FindByUserId(user.Id);
        if (userRole is null) return ResultResponse<ManageUser>.Failure("User-Role" + ConstantsResponse.NotFound);

        var systemRole = await _systemRoleRepository.GetById(userRole.RoleId);
        if (systemRole is null) return ResultResponse<ManageUser>.Failure("System-Role" + ConstantsResponse.NotFound);

        manageUser = new ManageUser
        {
            UserId = user.Id,
            UserName = user.FullName,
            Email = user.Email,
            Role = systemRole.Name,
            Autherize = user.Autherize,
        };
        return ResultResponse<ManageUser>.Success(manageUser);
    }

    public async Task<GeneralResponse>Update(ManageUser manageUser)
    {
        if (manageUser.Role is null) return new GeneralResponse(false,"Manageuser" + ConstantsResponse.NotFound);
        var systemRole = await _systemRoleRepository.GetByName(manageUser.Role);
        if (systemRole is null) return new GeneralResponse(false, "System-role" + ConstantsResponse.NotFound);

        var userRole = await _userRoleRepository.FindByUserId(manageUser.UserId);
        if (userRole is null) return new GeneralResponse(false, "User-role"+ConstantsResponse.NotFound);

        userRole.RoleId = systemRole.Id;

        var userRoleResponse = await _userRoleRepository.Update(userRole);
        if (!userRoleResponse.Flag) return userRoleResponse;

        if (manageUser is null || manageUser.Email is null || manageUser.UserName is null) return new GeneralResponse(false, "Manageuser" + ConstantsResponse.NotFound);
        var user = new AppUser
        {
            Id = manageUser.UserId,
            FullName = manageUser.UserName,
            Email = manageUser.Email,
            Autherize = manageUser.Autherize
        };

        var userAcountResponse = await _userAccountRepository.Update(user);
        return userAcountResponse;
    }

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var responseAppUser = await _userAccountRepository.DeleteById(id);
        if (!responseAppUser.Flag) return responseAppUser;

        var responseUserRole = await _userRoleRepository.DeleteByUserId(id);
        return responseUserRole;
    }

}
