using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class OtherBaseEntity
{
    public int Id { get; set; }

    [Required]
    [Range(1 , int.MaxValue, ErrorMessage = "Enter Valid Employee Id Value")]
    public int EmployeeId { get; set; }
}
