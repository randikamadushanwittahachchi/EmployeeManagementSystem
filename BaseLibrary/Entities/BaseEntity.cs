using System.ComponentModel.DataAnnotations;
namespace BaseLibrary.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Valid Name")]
    public string Name { get; set; } = string.Empty;
}
