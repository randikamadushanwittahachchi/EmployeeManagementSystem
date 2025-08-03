using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class EmployeeModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public EmployeeModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }

    public void ShowDialog(Func<Employee,Task> funEmployee, string titel, string buttonTitel)
    {
        var parameter = new ModalParameters()
            .Add(nameof(EmployeeDialog.FunEmployee), funEmployee)
            .Add(nameof(EmployeeDialog.Titel), titel)
            .Add(nameof(EmployeeDialog.ButtonTitel), buttonTitel);
        _modal.Show<EmployeeDialog>(parameter, _option);
    }

    public void ShowDialog(Func<Employee,Task> funEmployee,Employee employee, string titel, string buttonTitel)
    {
        var parameter = new ModalParameters()
            .Add(nameof(EmployeeDialog.FunEmployee), funEmployee)
            .Add(nameof(EmployeeDialog.Employee), employee)
            .Add(nameof(EmployeeDialog.Titel), titel)
            .Add(nameof(EmployeeDialog.ButtonTitel), buttonTitel);
        _modal.Show<EmployeeDialog>(parameter, _option);
    }

}
