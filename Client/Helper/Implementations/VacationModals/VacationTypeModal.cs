using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.VacationDialog;

namespace Client.Helper.Implementations.VacationModals;

public class VacationTypeModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public VacationTypeModal(IModalService modal)
    {
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };

        _modal = modal;
        _option = option;
    }


    public void Show(Func<VacationType,Task> funcVacationType,String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(VacationTypeDialog.FuncVacation), funcVacationType)
            .Add(nameof(VacationTypeDialog.Titel), titel)
            .Add(nameof(VacationTypeDialog.Button), button);
        _modal.Show<VacationTypeDialog>(parameter, _option);
    }
    public void Show(Func<VacationType, Task> funcVacationType,VacationType vacationType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(VacationTypeDialog.FuncVacation), funcVacationType)
            .Add(nameof(VacationTypeDialog.VacationType), vacationType)
            .Add(nameof(VacationTypeDialog.Titel), titel)
            .Add(nameof(VacationTypeDialog.Button), button);
        _modal.Show<VacationTypeDialog>(parameter, _option);
    }
}
