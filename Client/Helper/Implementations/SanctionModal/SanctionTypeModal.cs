using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.SanctionDialog;

namespace Client.Helper.Implementations.SanctionModal;

public class SanctionTypeModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public SanctionTypeModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };
        _modal = modal;
        _option = option;
    }

    public void Show(Func<SanctionType,Task> funcSanctionType,SanctionType sanctionType,String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(SanctionTypeDialog.FuncSanctionType), funcSanctionType)
            .Add(nameof(SanctionTypeDialog.SanctionType), sanctionType)
            .Add(nameof(SanctionTypeDialog.Titel), titel)
            .Add(nameof(SanctionTypeDialog.Button), button);
        _modal.Show<SanctionTypeDialog>(parameter, _option);
    }
    public void Show(Func<SanctionType, Task> funcSanctionType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(SanctionTypeDialog.FuncSanctionType), funcSanctionType)
            .Add(nameof(SanctionTypeDialog.Titel), titel)
            .Add(nameof(SanctionTypeDialog.Button), button);
        _modal.Show<SanctionTypeDialog>(parameter, _option);
    }
}
