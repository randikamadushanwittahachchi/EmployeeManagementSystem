using System.ComponentModel.DataAnnotations;
namespace BaseLibrary.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }
}
