using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace style_catalog.Models;

public class Account : IdentityUser{
  [Key]
  public int Id { get; set; }
  public string UserName { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
  public string ConfirmPassword { get; set; }
}