using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class VacationType:BaseEntity
{
    //Realationship:one to many
    [JsonIgnore]
    public List<Vacation>? Vacation { get; set; }
}