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
    public OverTimeType? OverTimeType { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Enter Vaid Over time type")]
    public int OverTimeTypeId { get; set; }


}
