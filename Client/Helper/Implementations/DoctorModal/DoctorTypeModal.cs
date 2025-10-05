using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.DoctorDialog;

namespace Client.Helper.Implementations.DoctorModal;

public class DoctorTypeModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;

    public DoctorTypeModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };

        _option = option;
        _modal = modal;
    }

    public void Show(Func<DoctorType, Task> funcDoctorType,DoctorType doctorType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(DoctorTypeDialog.FuncDoctorType), funcDoctorType)
            .Add(nameof(DoctorTypeDialog.DoctorType), doctorType)
            .Add(nameof(DoctorTypeDialog.Titel), titel)
            .Add(nameof(DoctorTypeDialog.Button), button);
        _modal.Show<DoctorTypeDialog>(parameter, _option);
    }
    public void Show(Func<DoctorType, Task> funcDoctorType, String titel, String button)
    {
        var parameter = new ModalParameters()
            .Add(nameof(DoctorTypeDialog.FuncDoctorType), funcDoctorType)
            .Add(nameof(DoctorTypeDialog.Titel), titel)
            .Add(nameof(DoctorTypeDialog.Button), button);
        _modal.Show<DoctorTypeDialog>(parameter, _option);
    }
}
