using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class CountryModal : IGenericModal<Country>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public CountryModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog(Func<Country, Task> save, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(CountryDialog.FunCountry), save)
            .Add(nameof(CountryDialog.Titel), titel)
            .Add(nameof(CountryDialog.Button), button);
        _modal.Show<CountryDialog>(parameters, _option);
    }
    public void ShowDialog(Func<Country, Task> edit, Country country, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(CountryDialog.FunCountry), edit)
            .Add(nameof(CountryDialog.Country), country)
            .Add(nameof(CountryDialog.Titel), titel)
            .Add(nameof(CountryDialog.Button), button);
        _modal.Show<CountryDialog>(parameters, _option);
    }
}
