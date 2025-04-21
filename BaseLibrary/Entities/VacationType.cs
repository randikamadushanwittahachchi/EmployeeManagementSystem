namespace BaseLibrary.Entities;

public class VacationType:BaseEntity
{
    //Realationship:one to many
    public List<Vacation>? Vacation { get; set; }
}