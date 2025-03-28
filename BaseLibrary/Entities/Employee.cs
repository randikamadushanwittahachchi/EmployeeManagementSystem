﻿namespace BaseLibrary.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? CivilId { get; set; }
    public string? FileName { get; set; }
    public string? JobName { get; set; }
    public string? FullName { get; set; }
    public string? Addrese { get; set; }
    public string? Photo {  get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Others { get; set; }

    //Relationship : Many to One
    public GeneralDepartment? GeneralDepartment {  get; set; }
    public int? GeneralDepartmentId { get; set; }
    public Department? Depatment {  get; set; }
    public int? DepartmentId { get; set; }
    public Branch? Branch { get; set; }
    public int? BranchId { get; set; }
    public Town? Town { get; set; }
    public int? TownId { get; set; }
}
