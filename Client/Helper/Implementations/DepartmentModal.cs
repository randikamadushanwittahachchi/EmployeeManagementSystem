using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class DepartmentModal : IGenericModal<Department>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    // Constructor inject IModalService
    public DepartmentModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }

    public void ShowDialog(Func<Department, Task> saveDepartment, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DepartmentDialog.FuncDepartment), saveDepartment)
            .Add(nameof(DepartmentDialog.Titel), titel)
            .Add(nameof(DepartmentDialog.Button), button);
        _modal.Show<DepartmentDialog>(parameters, _option);
    }

    public void ShowDialog(Func<Department, Task> editDepartment, Department department, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DepartmentDialog.FuncDepartment), editDepartment)
            .Add(nameof(DepartmentDialog.Department), department)
            .Add(nameof(DepartmentDialog.Titel), titel)
            .Add(nameof(DepartmentDialog.Button), button);
        _modal.Show<DepartmentDialog>(parameters, _option);
    }
}
