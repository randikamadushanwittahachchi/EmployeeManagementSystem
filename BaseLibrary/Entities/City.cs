using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class City : BaseEntity
{
    //Relationship : one to Many
    public Country? Country { get; set; }
    [Required(ErrorMessage = "Select Country")]
    [Range(1,int.MaxValue,ErrorMessage = "Select Valid Country")]
    public int CountryId { get; set; }

    //Ralationship : Many to One
    [JsonIgnore]
    public List<Town>? Towns { get; set; }

}
