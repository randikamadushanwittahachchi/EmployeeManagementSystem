using BaseLibrary.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using Client.Helper.Constracts;
using Client.Pages.Shared;

namespace Client.Helper.Implementations;

public class BranchModal : IGenericModal<Branch>
{
    private readonly IModalService _modal;
    private readonly ModalOptions _option;
    public BranchModal(IModalService modal)
    {
        _modal = modal;
        var option = new ModalOptions()
        {
            UseCustomLayout = true
        };
        _option = option;
    }
    public void ShowDialog(Func<Branch, Task> save, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(BranchDialog.FunBranch), save)
            .Add(nameof(BranchDialog.Titel), titel)
            .Add(nameof(BranchDialog.Button), button);
        _modal.Show<BranchDialog>(parameters, _option);
    }
    public void ShowDialog(Func<Branch, Task> edit, Branch branch, string titel, string button)
    {
        var parameters = new ModalParameters()
            .Add(nameof(BranchDialog.FunBranch), edit)
            .Add(nameof(BranchDialog.Branch), branch)
            .Add(nameof(BranchDialog.Titel), titel)
            .Add(nameof(BranchDialog.Button), button);
        _modal.Show<BranchDialog>(parameters, _option);
    }
}