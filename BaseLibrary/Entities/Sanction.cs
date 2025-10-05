using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Sanction:OtherBaseEntity
{
    [Required(ErrorMessage = "Enter Valid Date")]
    public DateTime Date { get; set; }
    [Required(AllowEmptyStrings = false,ErrorMessage = "Enter Valid Sanction Name")]
    public String SanctionName { get; set; } = String.Empty;
    [Required(ErrorMessage = "Enter Valid Sanction Date")]
    public DateTime SanctionDate { get; set; }

    //Relationship: many to one
    public SanctionType? SanctionType { get; set; }
    [Required]
    [Range(1 , int.MaxValue, ErrorMessage = "Enter Valid Saction Type")]
    public int SanctionTypeId { get; set; }

}
