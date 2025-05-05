using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace Client.Helper.Constracts;

public interface IModalDialog
{
    void ShowDialog(string? header, string? message);
    Task ShowDialog(string? header, string? message, Func<Task>? taskCallBack = null);
}
