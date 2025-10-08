using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.AdministrationPage;
using Client.Pages.Shared.DoctorDialog;

namespace Client.Helper.Implementations.AdministrationModal;

public class AdministrationModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public AdministrationModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };
        _option = option;
        _modal = modal;
    }


    public void ShowDialog(Func<ManageUser, Task> funcManageUser, ManageUser manageUser)
    {
        var parameters = new ModalParameters()
            .Add(nameof(AdministrationDialog.FuncManageUser), funcManageUser)
            .Add(nameof(AdministrationDialog.ManageUser), manageUser);

        _modal.Show<AdministrationDialog>(parameters, _option);
    }
}
