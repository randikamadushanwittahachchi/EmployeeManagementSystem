using BaseLibrary.Entities;

namespace Client.State;

public class AllState
{
    public Action? Action { get; set; }
    public bool ShowManageUser { get; set; }
    public bool ShowEmployee { get; set; }
    public bool ShowGeneralDepartment { get; set; }
    public bool ShowDepartment { get; set; }
    public bool ShowBranch { get; set; }
    public bool ShowTown { get; set; }
    public bool ShowCity { get; set; }
    public bool ShowCountry { get; set; }
    public bool ShowDoctor { get; set; }
    public bool ShowDoctorType { get; set; }
    public bool ShowVacation { get; set; }
    public bool ShowVacationType { get; set; }
    public bool ShowOverTime { get; set; }
    public bool ShowOverTimeType { get; set; }
    public bool ShowSanction { get; set; }
    public bool ShowSanctionType { get; set; }

    // Set Data

    public List<Doctor> Doctors { get; private set; } = new();
    public List<Vacation> Vacations { get; private set; } = new();
    public List<Sanction> Sanctions { get; private set; } = new();
    public List<OverTime> OverTimes { get; private set; } = new();
    public List<Employee> Employees { get; private set; } = new();

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
        ShowBranch = true;
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
    public void ManageUserClick()
    {
        ResetALlDepartments();
        ShowManageUser = true;
        Action?.Invoke();
    }
    // Employee
    public void EmployeeClick()
    {
        ResetALlDepartments();
        ShowEmployee = true;
        Action!.Invoke();
    }
    public void SetEmployee(List<Employee> employees)
    {
        Employees = employees;
        Action!.Invoke();
    }

    // Doctor
    public void DoctorClick()
    {
        ResetALlDepartments();
        ShowDoctor = true;
        Action!.Invoke();
    }
    public void SetDoctor(List<Doctor> doctors)
    {
        Doctors = doctors;
        Action!.Invoke();
    }


    // Doctor-Type
    public void DoctorTypeClick()
    {
        ResetALlDepartments();
        ShowDoctorType = true;
        Action!.Invoke();
    }

    // Vacation
    public void VacationClick()
    {
        ResetALlDepartments();
        ShowVacation = true;
        Action!.Invoke();
    }
    public void SetVacation(List<Vacation> vacations)
    {
        Vacations = vacations;
        Action!.Invoke();
    }
    // VacationType
    public void VacationTypeClick()
    {
        ResetALlDepartments();
        ShowVacationType = true;
        Action!.Invoke();
    }
    // OverTime
    public void OverTimeClick()
    {
        ResetALlDepartments();
        ShowOverTime = true;
        Action!.Invoke();
    }
    public void SetOverTime(List<OverTime> overTimes)
    {
        OverTimes = overTimes;
        Action!.Invoke();
    }

    // OverTimeType
    public void OverTimeTypeClick()
    {
        ResetALlDepartments();
        ShowOverTimeType = true;
        Action!.Invoke();
    }

    // Sanction
    public void SanctionClick()
    {
        ResetALlDepartments();
        ShowSanction = true;
        Action!.Invoke();
    }
    public void SetSanction(List<Sanction> sanctions)
    {
        Sanctions = sanctions;
        Action!.Invoke();
    }
    // SanctionType
    public void SanctionTypeClick()
    {
        ResetALlDepartments();
        ShowSanctionType = true;
        Action!.Invoke();
    }

    // Reset all 
    public void ResetALlDepartments()
    {
        ShowGeneralDepartment = false;
        ShowDepartment = false;
        ShowBranch = false;
        ShowTown = false;
        ShowCity = false;
        ShowCountry = false;
        ShowManageUser = false;
        ShowEmployee = false;
        ShowDoctor = false;
        ShowDoctorType = false;
        ShowVacation = false;
        ShowVacationType = false;
        ShowOverTime = false;
        ShowOverTimeType = false;
        ShowSanction = false;
        ShowSanctionType = false;
    }

}
