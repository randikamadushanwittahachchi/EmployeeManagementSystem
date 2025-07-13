using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Employee:BaseEntity
{
    [Required]
    public string CivilId { get; set; } = null!;
    [Required]
    public string FileName { get; set; } = null!;
    [Required]
    public string JobName { get; set; } = null!;
    [Required]
    public string Addrese { get; set; } = null!;
    [Required]
    public string Photo { get; set; } = null!;
    [Required,DataType(DataType.PhoneNumber)]
    public string TelephoneNumber { get; set; } = null!;
    public string? Others { get; set; }

    //Relationship : Many to One
    public Branch? Branch { get; set; }
    public int? BranchId { get; set; }
    public Town? Town { get; set; }
    public int? TownId { get; set; }
}
