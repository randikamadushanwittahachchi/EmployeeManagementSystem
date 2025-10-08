using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.DoctorDialog;
using Client.Pages.Shared.ProfileDialog;

namespace Client.Helper.Implementations.ProfileModal;

public class ProfileEditModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public ProfileEditModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };

        _option = option;
        _modal = modal;
    }

    public void Show(Func<ManageUser , Task> funcManageUser, ManageUser manageUser, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(ProfileEditDialog.FuncManageUser), funcManageUser)
            .Add(nameof(ProfileEditDialog.ManageUser), manageUser)
            .Add(nameof(ProfileEditDialog.Titel), titel)
            .Add(nameof(ProfileEditDialog.Button), button);
        _modal.Show<ProfileEditDialog>(parameter, _option);
    }
}
