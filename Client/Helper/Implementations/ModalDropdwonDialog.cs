using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class ModalDropdwonDialog : IModalDropdwonDialog
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    // Constructor inject IModalService
    public ModalDropdwonDialog(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }

    public void ShowDialog(Func<Department, Task> saveDepartment, string titel)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DropdwonDialog.FuncDepartment), saveDepartment)
            .Add(nameof(DropdwonDialog.Titel), titel);
        _modal.Show<DropdwonDialog>(parameters, _option);
    }

    public void ShowDialog(Func<Department, Task> editDepartment, Department department, string titel)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DropdwonDialog.FuncDepartment), editDepartment)
            .Add(nameof(DropdwonDialog.Department), department)
            .Add(nameof(DropdwonDialog.Titel), titel);
        _modal.Show<DropdwonDialog>(parameters, _option);
    }
}
