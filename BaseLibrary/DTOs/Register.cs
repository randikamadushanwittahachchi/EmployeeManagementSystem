using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class Register : AccountBase
{
    [Required]
    [MinLength(4)]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    [Required]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfimPassword { get; set; } = null!;
}
