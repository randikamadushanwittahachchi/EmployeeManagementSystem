using Blazored.Modal;

namespace Client.Helper.Constracts;

public interface IModalDialog
{
    void ShowDialog(string? header, string? message);
    void ShowDialog(string? header, string? message, Func<Task>? taskCallBack = null);
    void ShowDialog(string? header, string? message, Action? callBack = null);
}
