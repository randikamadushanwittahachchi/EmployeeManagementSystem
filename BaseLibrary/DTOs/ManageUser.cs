

using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class ManageUser
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Full Name is required.")]
    [DataType(DataType.Text)]
    [MinLength(4)]
    [MaxLength(100)]
    public String UserName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress)]
    public String Email { get; set; } = String.Empty;

    [Required(ErrorMessage = "Role is required.")]
    [DataType(DataType.Text)]
    public String Role { get; set; } = String.Empty;

    [Required(ErrorMessage = "Required to Selection")]
    public bool IsAutherize { get; set; } = false;
}
