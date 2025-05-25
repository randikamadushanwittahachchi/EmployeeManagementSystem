using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared;
using Client.Helper.Constracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Components;
using BaseLibrary.Responses;

namespace Client.Helper.Implementations;

public class GeneralDeparmentModal : IGenericModal<GeneralDepartment>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public GeneralDeparmentModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }


    public void ShowDialog(Func<GeneralDepartment,Task> addGeneralDepartment, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(GeneralDepartmentDialog.FuncGeneralDepartment), addGeneralDepartment)
            .Add(nameof(GeneralDepartmentDialog.Titel), titel)
            .Add(nameof(GeneralDepartmentDialog.Button), button);
        _modal.Show<GeneralDepartmentDialog>(parameters,_option);
    }
    public void ShowDialog(Func<GeneralDepartment, Task> editGeneralDepartment, GeneralDepartment generalDepartment, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(GeneralDepartmentDialog.FuncGeneralDepartment), editGeneralDepartment)
            .Add(nameof(GeneralDepartmentDialog.GeneralDepartment), generalDepartment)
            .Add(nameof(GeneralDepartmentDialog.Titel), titel)
            .Add(nameof(GeneralDepartmentDialog.Button), button);
        _modal.Show<GeneralDepartmentDialog>(parameters, _option);
    }

}
