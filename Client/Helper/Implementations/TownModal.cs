using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class TownModal : IGenericModal<Town>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public TownModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog(Func<Town, Task> save, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(TownDialog.FunTown), save)
            .Add(nameof(TownDialog.Titel), titel)
            .Add(nameof(TownDialog.Button), button);
        _modal.Show<TownDialog>(parameters, _option);
    }
    public void ShowDialog(Func<Town, Task> edit, Town town, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(TownDialog.FunTown), edit)
            .Add(nameof(TownDialog.Town), town)
            .Add(nameof(TownDialog.Titel), titel)
            .Add(nameof(TownDialog.Button), button);
        _modal.Show<TownDialog>(parameters, _option);
    }
}
