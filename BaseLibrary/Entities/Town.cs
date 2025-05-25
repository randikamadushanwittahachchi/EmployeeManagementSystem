using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class Town : BaseEntity
{
    //Relationship : one to Many
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }

    //Relationship : many to one
    public City? City { get; set; }
    public int? CityId { get; set; }


}