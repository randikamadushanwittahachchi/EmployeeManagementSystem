using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;
using Client.Helper.Constracts;

namespace Client.Helper.Implementations;

public class ModalInputDialog : IModalInputDialog
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public ModalInputDialog(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog()
    {
        _modal.Show<DepartmentDialog>(_option);
    }
}
