using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.OverTimeDialog;

namespace Client.Helper.Implementations.OverTimeModal;

public class OverTimeTypeModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public OverTimeTypeModal(IModalService modal)
    {
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };

        _modal = modal;
        _option = option;
    }

    public void ShowModal(Func<OverTimeType, Task> funcOverTimeType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(OverTimeTypeDialog.FuncOverTimeType), funcOverTimeType)
            .Add(nameof(OverTimeTypeDialog.Titel), titel)
            .Add(nameof(OverTimeTypeDialog.Button), button);

        _modal.Show<OverTimeTypeDialog>(parameter, _option);
    }
    public void ShowModal(Func<OverTimeType, Task> funcOverTimeType,OverTimeType overTimeType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(OverTimeTypeDialog.FuncOverTimeType), funcOverTimeType)
            .Add(nameof(OverTimeTypeDialog.OverTimeType), overTimeType)
            .Add(nameof(OverTimeTypeDialog.Titel), titel)
            .Add(nameof(OverTimeTypeDialog.Button), button);

        _modal.Show<OverTimeTypeDialog>(parameter, _option);
    }
}
