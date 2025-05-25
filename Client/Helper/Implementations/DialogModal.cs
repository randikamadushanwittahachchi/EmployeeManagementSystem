using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;
using Client.Helper.Constracts;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Client.Helper.Implementations;
public class DialogModal : IDialogModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public DialogModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog(string? header, string? message)
    {
        var paramter = new ModalParameters()
        .Add(nameof(Dialog.Header), header)
        .Add(nameof(Dialog.Message), message);
        _modal.Show<Dialog>(paramter, _option);
    }
    public async Task ShowDialog(string? header, string? message, Func<Task>? taskCallBack = null)
    {
        var paramter = new ModalParameters()
        .Add(nameof(Dialog.Header), header)
        .Add(nameof(Dialog.Message), message)
        .Add(nameof(Dialog.TaskCallBack), taskCallBack);
        var modalRef = _modal.Show<Dialog>(paramter, _option);

        var result = await modalRef.Result;

        if(!result.Cancelled && taskCallBack != null && result.Data is bool ok && ok)
        {
            await taskCallBack();
        }

    }
}
