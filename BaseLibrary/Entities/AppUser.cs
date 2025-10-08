using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class AppUser
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Full Name is required.")]
    [MinLength(4)]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public String FullName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress)]
    public String Email { get; set; } = String.Empty;

    [Required(ErrorMessage = "PassWord is required.")]
    [DataType(DataType.Password)]
    public String Password { get; set; } = String.Empty;

    [Required(ErrorMessage = "Required to Selection")]
    public bool Autherize { get; set; } = false;
}
