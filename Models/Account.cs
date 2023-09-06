using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace style_catalog.Models;

public class Account : IdentityUser{
  [Key]
  public int id { get; set; }
  public string? firstName { get; set; }
  public string? username { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  public string? password { get; set; }

  [DataType(DataType.Password)]
  [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
  public string? confirmPassword { get; set; }
}