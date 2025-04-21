using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Employee:BaseEntity
{
    [Required]
    public string CivilId { get; set; } = string.Empty;
    [Required]
    public string FileName { get; set; } = string.Empty;
    [Required]
    public string JobName { get; set; } = string.Empty;
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string Addrese { get; set; } = string.Empty;
    [Required]
    public string Photo { get; set; } = string.Empty;
    [Required,DataType(DataType.PhoneNumber)]
    public string TelephoneNumber { get; set; } = string.Empty;
    public string? Others { get; set; }

    //Relationship : Many to One
    [JsonIgnore]
    public Branch? Branch { get; set; }
    public int? BranchId { get; set; }
    [JsonIgnore]
    public Town? Town { get; set; }
    public int? TownId { get; set; }
}
