using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Vacation:OtherBaseEntity
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public int Days => (StartDate - EndDate).Days;

    //Realationship:Many to one
    [JsonIgnore]
    public VacationType? VacationType { get; set; }
    [Required]
    public int VacationTypeId { get; set; }
}
