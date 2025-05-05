using BaseLibrary.Entities;

namespace Client.Helper.Constracts
{
    public interface IModalDropdwonDialog
    {
        void ShowDialog(Func<Department, Task> saveDepartment, string titel);
        void ShowDialog(Func<Department, Task> editDepartment, Department department, string titel);
    }
}
