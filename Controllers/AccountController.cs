#nullable disable

using style_catalog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace style_catalog.Controllers;

[Route("Account/[action]")]
public class AccountController : Controller{
    private readonly UserManager<Account> _userManager;

    public AccountController(UserManager<Account> userManager){
      _userManager = userManager;
    }

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

    [HttpGet]
    public IActionResult Login(){
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Account accountModel){
      if(!ModelState.IsValid){
        return View(accountModel);
      }

      var userAccount = await _userManager.FindByNameAsync(accountModel.username);
      if(userAccount != null && 
         await _userManager.CheckPasswordAsync(userAccount, accountModel.password))
      {
        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, (userAccount.id).ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, userAccount.username));
        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                                      new ClaimsPrincipal(identity));
          
        return RedirectToAction(nameof(HomeController.Home), "Home");
      }
      else{
        ModelState.AddModelError("", "Invalid UserName or Password");
        return View();
      }
    }
}
