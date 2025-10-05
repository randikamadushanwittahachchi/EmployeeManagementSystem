using Blazored.Modal;
using Blazored.Modal.Services;
using BaseLibrary.Entities;
using Client.Pages.Shared.OverTimeDialog;

namespace Client.Helper.Implementations.OverTime;

public class OverTimeModal
{
    readonly IModalService _modal;
    readonly ModalOptions _option;
    public OverTimeModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };
        _option = option;
        _modal = modal;
    }

    public void Show(Func<BaseLibrary.Entities.OverTime, Task> funcOverTime,Employee employee,String titel,String button )
    {
        var parameters = new ModalParameters()
            .Add(nameof(OverTimeDialog.FuncOverTime), funcOverTime)
            .Add(nameof(OverTimeDialog.Employee), employee)
            .Add(nameof(OverTimeDialog.Titel), titel)
            .Add(nameof(OverTimeDialog.Button), button);

        _modal.Show<OverTimeDialog>(parameters, _option);
    }
    public void Show(Func<BaseLibrary.Entities.OverTime, Task> funcOverTime,BaseLibrary.Entities.OverTime overTime, String titel, String button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(OverTimeDialog.FuncOverTime), funcOverTime)
            .Add(nameof(OverTimeDialog.OverTime), overTime)
            .Add(nameof(OverTimeDialog.Titel), titel)
            .Add(nameof(OverTimeDialog.Button), button);

        _modal.Show<OverTimeDialog>(parameters, _option);
    }
}
