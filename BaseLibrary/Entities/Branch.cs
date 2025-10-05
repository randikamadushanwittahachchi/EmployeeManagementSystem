using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Branch : BaseEntity
{
    // many to one relationship with Department
    [Required(ErrorMessage = "Select Department")]
    [Range(1,int.MaxValue, ErrorMessage = "Select Valid Department")]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    // one to many relationship with Employee
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }
}