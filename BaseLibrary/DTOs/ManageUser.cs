

using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class ManageUser
{
    [Required]
    public int UserId { get; set; }
    [Required]
    [DataType(DataType.Text)]
    public string? UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Text)]
    public string? Role { get; set; }
    [Required]
    public bool IsAutherize { get; set; } = false;
}
