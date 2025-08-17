using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Employee:BaseEntity
{
    [Required(ErrorMessage = "Civil Id is required.")]
    public string CivilId { get; set; } = null!;

    [Required(ErrorMessage ="File Name is required.")]
    public string FileName { get; set; } = null!;

    [Required(ErrorMessage ="Job is required.")]
    public string JobName { get; set; } = null!;

    [Required(ErrorMessage ="Addrese is required.")]
    public string Addrese { get; set; } = null!;

    [Required(ErrorMessage ="Image is required")]
    public string Photo { get; set; } = null!;

    [Required(ErrorMessage = "Telephone Number is required."),DataType(DataType.PhoneNumber)]
    public string TelephoneNumber { get; set; } = null!;
    public string? Others { get; set; }

    //Relationship : Many to One
    public Branch? Branch { get; set; }
    [Required]
    [Range(1 , int.MaxValue, ErrorMessage = "BranchId must be a positive number.")]
    public int BranchId { get; set; }
    public Town? Town { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "TownId must be a positive number.")]
    public int TownId { get; set; }
}
