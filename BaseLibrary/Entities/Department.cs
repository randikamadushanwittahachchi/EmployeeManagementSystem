using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Department:BaseEntity
{
    // many to one relationship with GeneralDepartment
    public int GeneralDeparmentId { get; set; }
    public GeneralDepartment? GeneralDepartment { get; set; }

    // one to many relationship with Branch
    public List<Branch>? Branches { get; set; }
}