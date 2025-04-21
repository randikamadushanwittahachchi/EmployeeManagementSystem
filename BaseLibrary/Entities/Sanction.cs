using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Sanction:OtherBaseEntity
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string SanctionName { get; set; } = string.Empty;
    [Required]
    public DateTime SanctionDate { get; set; }

    //Relationship: many to one
    [JsonIgnore]
    public SanctionType? SanctionType { get; set; }
    [Required]
    public int SanctionTypeId { get; set; }

}
