using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Town : BaseEntity
{
    //Relationship : many to one
    public City? City { get; set; }
    [Required(ErrorMessage = "Select City")]
    [Range(1, int.MaxValue, ErrorMessage = "Select Valid City")]
    public int CityId { get; set; }
    //Relationship : one to Many
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }



}