using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Department:BaseEntity
{
    // many to one relationship with GeneralDepartment
    public int GeneralDeparmentId { get; set; }
    [JsonIgnore]
    public GeneralDepartment? GeneralDepartment { get; set; }

    // one to many relationship with Branch
    [JsonIgnore]
    public List<Branch>? Branches { get; set; }
}