using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;
using Client.Helper.Constracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Components;
using BaseLibrary.Responses;

namespace Client.Helper.Implementations;

public class ModalInputDialog : IModalInputDialog
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public ModalInputDialog(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }


    public void ShowDialog(Func<GeneralDepartment,Task> addGeneralDepartment, String titel)
    {
        var parameters = new ModalParameters()
            .Add(nameof(InputDialog.FuncGeneralDepartment), addGeneralDepartment)
            .Add(nameof(InputDialog.Titel), titel);
        _modal.Show<InputDialog>(parameters,_option);
    }
    public void ShowDialog(Func<GeneralDepartment, Task> editGeneralDepartment, GeneralDepartment generalDepartment, String titel)
    {
        var parameters = new ModalParameters()
            .Add(nameof(InputDialog.FuncGeneralDepartment), editGeneralDepartment)
            .Add(nameof(InputDialog.GeneralDepartment), generalDepartment)
            .Add(nameof(InputDialog.Titel), titel);
        _modal.Show<InputDialog>(parameters, _option);
    }

}
