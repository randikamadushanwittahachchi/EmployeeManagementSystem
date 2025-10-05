using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.SanctionDialog;
using System.Reflection.Metadata;

namespace Client.Helper.Implementations.SanctionModal;

public class SanctionModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public SanctionModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };
        _modal = modal;
        _option = option;
    }

    public void Show(Func<Sanction,Task> funcSanction,Employee employee,String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(SanctionDialog.FuncSanction), funcSanction)
            .Add(nameof(SanctionDialog.Employee), employee)
            .Add(nameof(SanctionDialog.Titel), titel)
            .Add(nameof(SanctionDialog.Button), button);
        _modal.Show<SanctionDialog>(parameter, _option);
    }
    public void Show(Func<Sanction, Task> funcSanction,Sanction sanction, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(SanctionDialog.FuncSanction), funcSanction)
            .Add(nameof(SanctionDialog.Sanction), sanction)
            .Add(nameof(SanctionDialog.Titel), titel)
            .Add(nameof(SanctionDialog.Button), button);
        _modal.Show<SanctionDialog>(parameter, _option);
    }

}
