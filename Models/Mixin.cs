#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace style_catalog.Models;

public class Mixin {
  public string Id { get; set; }
  public string AccountId { get; set; }
  [Required(ErrorMessage = "Mixin title is required")]
  public string name { get; set; }
  [Required(ErrorMessage = "Mixin property is required")]
  public string body { get; set; }
  public string arguments { get; set; }
}