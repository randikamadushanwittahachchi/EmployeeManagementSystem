using System.ComponentModel.DataAnnotations;
namespace BaseLibrary.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter Valid Name")]
    public string? Name { get; set; }
}
