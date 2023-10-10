using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using style_catalog.Models;
using style_catalog.Data;
using System.Security.Claims;

namespace style_catalog.Controllers;

[Route("Mixin/[action]/[id?]")]
public class MixinController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly DatabaseContext _context;
   
    public MixinController(UserManager<Account> userManager, 
                           DatabaseContext context){
    _userManager = userManager;
    _context = context;
    }

    public IActionResult List(){
      string userId = _userManager.GetUserId(User);
      var mixinList = _context.Mixin.Where(mixin => (
        mixin.AccountId == userId
      ));
      
      return View(mixinList);
    }

    [HttpGet("{id}")]   
    public IActionResult Select(string id){
      return View(ViewBag.id);
    }
}