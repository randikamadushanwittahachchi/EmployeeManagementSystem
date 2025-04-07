using Syncfusion.Blazor.Popups;
namespace Client.Service;

public class MyDialogService
{
    private readonly SfDialogService _dialogService;

    public MyDialogService(SfDialogService dialogService)
    {
        _dialogService = dialogService;
    }
    public async Task ShowAlert(string messege, string title)
    {
        await _dialogService.OpenAsync(
            new DialogOptions
            { 
                Title = title,
                Content = messege,

            });
    }


}
