using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Department:BaseEntity
{
    // many to one relationship with GeneralDepartment
    [Required(ErrorMessage = "Select General Department")]
    [Range(1, int.MaxValue,ErrorMessage = "Select Valid General Department")]
    public int GeneralDepartmentId { get; set; }
    public GeneralDepartment? GeneralDepartment { get; set; }

    // one to many relationship with Branch
    [JsonIgnore]
    public List<Branch>? Branches { get; set; }
}