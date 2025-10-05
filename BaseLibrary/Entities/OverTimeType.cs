using System.Text.Json.Serialization;

namespace BaseLibrary.Entities;

public class OverTimeType: BaseEntity
{
    //Realationship:one to many
    [JsonIgnore]
    public List<OverTime>? OverTime { get; set; }
}