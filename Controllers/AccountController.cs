#nullable disable

using System;
using style_catalog.Models;
using style_catalog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;

namespace style_catalog.Controllers;

[Route("Account/[action]")]
public class AccountController : Controller{
    private readonly UserManager<Account> _userManager;
    private readonly DatabaseContext _context;

    public AccountController(UserManager<Account> userManager, DatabaseContext context){
      _userManager = userManager;
      _context = context;
    }

    [HttpGet]
    public IActionResult Register(){
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Account model){
      if(!ModelState.IsValid){
        return View(model);
      }
      var user = new Account(){ UserName = model.UserName };
      var result = await _userManager.CreateAsync(user, model.password);
        
      if(!result.Succeeded){
        foreach (var error in result.Errors){
          ModelState.TryAddModelError(error.Code, error.Description);
        }
        return View(model);
      }
      else{
        await _context.Account.AddAsync(user);
        return RedirectToAction(nameof(HomeController.Home), "Home");
      }
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

      var userAccount = await _userManager.FindByNameAsync(accountModel.UserName);
      if(userAccount != null && 
         await _userManager.CheckPasswordAsync(userAccount, accountModel.password))
      {
        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, (userAccount.id).ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, userAccount.UserName));
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
