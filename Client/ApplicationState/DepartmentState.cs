namespace Client.ApplicationState;

public class DepartmentState
{
    public Action? GeneralDepartmentAction { get; set; }
    public bool ShowGeneralDepartment { get; set; }

    public void GeneralDepartmentClick()
    {
        ShowGeneralDepartment = true;
        GeneralDepartmentAction?.Invoke();
    }
    public void ResetALlDepartments()
    {
        ShowGeneralDepartment = false;
    }

}
