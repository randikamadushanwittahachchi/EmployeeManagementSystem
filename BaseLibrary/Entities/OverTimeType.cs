namespace BaseLibrary.Entities;

public class OverTimeType: BaseEntity
{
    //Realationship:one to many
    public List<OverTime>? OverTime { get; set; }
}