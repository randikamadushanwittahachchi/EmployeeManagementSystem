using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.VacationDialog;

namespace Client.Helper.Implementations.VacationModals;

public class VacationModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public VacationModal(IModalService modal)
    {
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };

        _modal = modal;
        _option = option;
    }

    public void Show(Func<Vacation,Task> funcVacation, Employee employee,String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(VacationDialog.FuncVacation), funcVacation)
            .Add(nameof(VacationDialog.Employee), employee)
            .Add(nameof(VacationDialog.Titel), titel)
            .Add(nameof(VacationDialog.Button), button);
        _modal.Show<VacationDialog>(parameter, _option);
    }
    public void Show(Func<Vacation, Task> funcVacation, Vacation vacation, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(VacationDialog.FuncVacation), funcVacation)
            .Add(nameof(VacationDialog.Vacation), vacation)
            .Add(nameof(VacationDialog.Titel), titel)
            .Add(nameof(VacationDialog.Button), button);
        _modal.Show<VacationDialog>(parameter, _option);
    }
}
