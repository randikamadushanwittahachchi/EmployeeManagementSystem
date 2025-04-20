using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;
using Client.Helper.Constracts;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Client.Helper.Implementations;
public class ModalDialog : IModalDialog
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public ModalDialog(IModalService modal)
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
        .Add(nameof(DialogShared.Header), header)
        .Add(nameof(DialogShared.Message), message);
        _modal.Show<DialogShared>(paramter, _option);
    }
    public void ShowDialog(string? header, string? message, Func<Task>? taskCallBack = null)
    {
        var paramter = new ModalParameters()
        .Add(nameof(DialogShared.Header), header)
        .Add(nameof(DialogShared.Message), message)
        .Add(nameof(DialogShared.TaskCallBack), taskCallBack);
        _modal.Show<DialogShared>(paramter, _option);
    }
    public void ShowDialog(string? header, string? message,Action? callBack = null)
    {
        var paramter = new ModalParameters()
        .Add(nameof(DialogShared.Header), header)
        .Add(nameof(DialogShared.Message), message)
        .Add(nameof(DialogShared.CallBack), callBack);
        _modal.Show<DialogShared>(paramter, _option);
    }
}
