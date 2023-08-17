using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace style_catalog.Models;

public class Account : IdentityUser{
  public int id { get; set; }
  public string? firstName { get; set; }
  public string? username { get; set; }
  [JsonIgnore]
  public string? passwordHash { get; set; }
}