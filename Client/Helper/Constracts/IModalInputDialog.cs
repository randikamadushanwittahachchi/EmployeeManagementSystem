using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Components;

namespace Client.Helper.Constracts
{
    public interface IModalInputDialog
    {
        void ShowDialog(Func<GeneralDepartment, Task> saveGeneralDepartment);
        void ShowDialog(Func<GeneralDepartment, Task> editGeneralDepartment, GeneralDepartment generalDepartment);
    }
}
