#nullable disable

using style_catalog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace style_catalog.Controllers;

[Route("Account/[action]")]
public class AccountController : Controller{
    private readonly UserManager<Account> _userManager;

    [HttpGet]
    public IActionResult Register(){
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Account accountModel){
      if(!ModelState.IsValid){
        return View(accountModel);
      }
      
      var userAccount = new Account();
      var result = await _userManager
        .CreateAsync(userAccount, accountModel.password);
      
      if(!result.Succeeded){
        foreach (var error in result.Errors){
            ModelState.TryAddModelError(error.Code, error.Description);
        }
        return View(accountModel);
      }
      await _userManager.AddToRoleAsync(userAccount, "Visitor");

      return RedirectToAction(nameof(HomeController.Home), "Home");
    }
}
