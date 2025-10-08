using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class Register : AccountBase
{
    [Required]
    [DataType(DataType.Text)]
    [MinLength(4)]
    [MaxLength(100)]
    public String FullName { get; set; } = String.Empty;

    [Required]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public String ConfimPassword { get; set; } = String.Empty;
}
