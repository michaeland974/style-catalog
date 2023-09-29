using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace style_catalog.Models;

public class Account : IdentityUser{
  [Key]
  public override string Id { get; set; }
  public override string UserName { get; set; } 

  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [DataType(DataType.Password)]
  [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
  public string ConfirmPassword { get; set; }
}