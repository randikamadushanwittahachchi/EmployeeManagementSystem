using BaseLibrary.DTOs;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class ManageUserModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public ManageUserModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }


    public void ShowDialog(Func<ManageUser, Task> save, string titel, string button, bool showEditEmail)
    {
        var Parameter = new ModalParameters()
            .Add(nameof(ManageUserDialog.FunManageUser),save)
            .Add(nameof(ManageUserDialog.Titel),titel)
            .Add(nameof(ManageUserDialog.Button), button)
            .Add(nameof(ManageUserDialog.ShowEditEmail), showEditEmail);
        _modal.Show<ManageUserDialog>(Parameter, _option);
    }

    public void ShowDialog(Func<ManageUser, Task> edit, ManageUser manageUser, string titel, string button)
    {
        var Parameter = new ModalParameters()
            .Add(nameof(ManageUserDialog.FunManageUser),edit)
            .Add(nameof(ManageUserDialog.ManageUser),manageUser)
            .Add(nameof(ManageUserDialog.Titel), titel)
            .Add(nameof(ManageUserDialog.Button), button);
        _modal.Show<ManageUserDialog>(Parameter, _option);
    }
}
