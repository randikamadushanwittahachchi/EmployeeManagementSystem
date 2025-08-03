using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class ViewModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public ViewModal(IModalService modal)
    {
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
        _modal = modal;
    }

    public void ShowModal(Employee employee)
    {
        var parameter = new ModalParameters()
            .Add(nameof(ViewDialog.Employee), employee);
        _modal.Show<ViewDialog>( parameter, _option);
    }
}
