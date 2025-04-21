using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Country : BaseEntity
{
    //Relationship : one to Many
    [JsonIgnore]
    public List<City>? Citys { get; set; }
}
