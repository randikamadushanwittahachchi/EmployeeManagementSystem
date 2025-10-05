using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Vacation:OtherBaseEntity
{
    [Required(ErrorMessage = "Data is Required")]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "Data is Required")]
    public DateTime EndDate { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Valid Data")]
    public String VacationName { get; set; } = String.Empty;
    public int Days => (StartDate - EndDate).Days;


    //Realationship:Many to one
    public VacationType? VacationType { get; set; }
    [Required(ErrorMessage = "Need Doctor Type")]
    [Range(1, int.MaxValue, ErrorMessage = "Enter Valid Doctor Type")]
    public int VacationTypeId { get; set; }
}
