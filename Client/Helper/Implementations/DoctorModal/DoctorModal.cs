using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Pages.Shared.DoctorDialog;

namespace Client.Helper.Implementations.DoctorModal;

public class DoctorModal
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public DoctorModal(IModalService modal)
    {
        var option = new ModalOptions
        {
            UseCustomLayout = true
        };
        _option = option;
        _modal = modal;
    }


    public void ShowDialog(Func<Doctor, Task> funcDoctor, Doctor doctor, String titel, String button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DoctorDialog.FuncDoctor), funcDoctor)
            .Add(nameof(DoctorDialog.Doctor), doctor)
            .Add(nameof(DoctorDialog.Titel), titel)
            .Add(nameof(DoctorDialog.Button), button);
        _modal.Show<DoctorDialog>(parameters, _option);
    }
    public void ShowDialog(Func<Doctor, Task> funcDoctor, Employee employee, String titel, String button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(DoctorDialog.FuncDoctor), funcDoctor)
            .Add(nameof(DoctorDialog.Employee), employee)
            .Add(nameof(DoctorDialog.Titel), titel)
            .Add(nameof(DoctorDialog.Button), button);
        _modal.Show<DoctorDialog>(parameters, _option);
    }
}
