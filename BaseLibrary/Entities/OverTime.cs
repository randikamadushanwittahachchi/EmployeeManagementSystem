using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;
public class OverTime:OtherBaseEntity
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public int Days => (StartDate - EndDate).Days;

    //Realationship:Many to One
    [JsonIgnore]
    public OverTimeType? OverTimeType { get; set; }
    [Required]
    public int? OverTimeTypeId { get; set; }


}
