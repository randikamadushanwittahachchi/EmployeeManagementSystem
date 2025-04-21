using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class SanctionType:BaseEntity
{
    //Relationship : one to many
    [JsonIgnore]
    public List<Sanction>? Sanctions {  get; set; }

}