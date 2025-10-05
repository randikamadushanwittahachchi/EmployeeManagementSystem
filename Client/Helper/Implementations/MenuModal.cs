using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class MenuModal
{
    private readonly ModalOptions _option;
    private readonly IModalService _modal;
    public MenuModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }

    public void ShowDialog(Employee employee,Func<Task> funcGetAll)
    {
        var parameter = new ModalParameters()
            .Add(nameof(MenuDialog.Employee), employee)
            .Add(nameof(MenuDialog.FuncGetAll), funcGetAll);
        _modal.Show<MenuDialog>(parameter, _option);
    }
}
