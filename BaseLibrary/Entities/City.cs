using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class City : BaseEntity
{
    //Ralationship : Many to One
    [JsonIgnore]
    public List<Town>? Towns { get; set; }

    //Relationship : one to Many
    public Country? Country { get; set; }
    public int? CountryId { get; set; }
}
