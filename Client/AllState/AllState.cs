namespace Client.ApplicationState;

public class AllState
{
    public Action? Action { get; set; }
    public bool ShowUser { get; set; }
    public bool ShowEmployee { get; set; }
    public bool ShowGeneralDepartment { get; set; }
    public bool ShowDepartment { get; set; }
    public bool SHowBranch { get; set; }
    public bool ShowTown { get; set; }
    public bool ShowCity { get; set; }
    public bool ShowCountry { get; set; }

    // GeneralDepartment
    public void GeneralDepartmentClick()
    {
        ResetALlDepartments();
        ShowGeneralDepartment = true;
        Action?.Invoke();
    }

    // Department
    public void DepartmentClick()
    {
        ResetALlDepartments();
        ShowDepartment = true;
        Action?.Invoke();
    }

    // Branch
    public void BranchClick()
    {
        ResetALlDepartments();
        SHowBranch = true;
        Action?.Invoke();
    }

    // Town
    public void TownClick()
    {
        ResetALlDepartments();
        ShowTown = true;
        Action?.Invoke();
    }

    // City
    public void CityClick()
    {
        ResetALlDepartments();
        ShowCity = true;
        Action?.Invoke();
    }

    // Country
    public void CountryClick()
    {
        ResetALlDepartments();
        ShowCountry = true;
        Action?.Invoke();
    }
    // User
    public void UserClick()
    {
        ShowUser = true;
        Action?.Invoke();
    }
    // Employee
    public void EmployeeClick()
    {
        ShowEmployee = true;
        Action?.Invoke();
    }

    // Reset all 
    public void ResetALlDepartments()
    {
        ShowGeneralDepartment = false;
        ShowDepartment = false;
        SHowBranch = false;
        ShowTown = false;
        ShowCity = false;
        ShowCountry = false;
    }

}
