namespace style_catalog.Models;

using System.Text.Json.Serialization;

public enum Role {Admin, User};

public class User{
  public int Id { get; set; }
  public string? FirstName { get; set; }
  public string? username { get; set; }
  public Role Role { get; set; }

  [JsonIgnore]
  public string? PasswordHash { get; set; }
}