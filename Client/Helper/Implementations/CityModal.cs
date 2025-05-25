using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class CityModal : IGenericModal<City>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public CityModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog(Func<City, Task> save, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(CityDialog.FunCity), save)
            .Add(nameof(CityDialog.Titel), titel)
            .Add(nameof(CityDialog.Button), button);
        _modal.Show<CityDialog>(parameters, _option);
    }
    public void ShowDialog(Func<City, Task> edit, City city, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(CityDialog.FunCity), edit)
            .Add(nameof(CityDialog.City), city)
            .Add(nameof(CityDialog.Titel), titel)
            .Add(nameof(CityDialog.Button), button);
        _modal.Show<CityDialog>(parameters, _option);
    }
}
