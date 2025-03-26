using System.Text.Json.Serialization;
namespace BaseLibrary.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    //Relationship : One to Many
    [JsonIgnore]
    public virtual List<Employee>? Employees { get; set; }
}
