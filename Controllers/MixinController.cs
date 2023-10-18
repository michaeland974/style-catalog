using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using style_catalog.Models;
using style_catalog.Data;
using System.Security.Claims;

namespace style_catalog.Controllers;

[Route("Mixin/[action]")]
public class MixinController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly DatabaseContext _context;
   
    public MixinController(UserManager<Account> userManager, 
                           DatabaseContext context){
    _userManager = userManager;
    _context = context;
    }

    [HttpGet]
    public IActionResult Create(){
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Mixin model){
      if(!ModelState.IsValid){
        return View(model);
      }

      var userId = _userManager.GetUserId(User);
      var mixin = new Mixin(){
        AccountId = userId,
        Id = Guid.NewGuid().ToString(),
        name = model.name,
        body = model.body,
        arguments = model.arguments
      };
      var result = await _context.Mixin.AddAsync(mixin);
      
      if(result is null){
        return View(model);
      }
      else{
        await _context.SaveChangesAsync();
        return RedirectToAction("List");
      }
    }

    [HttpGet]
    public IActionResult List(){
      var userId = _userManager.GetUserId(User);
      var mixins = _context.Mixin.Where(mixin => (
        mixin.AccountId == userId
      ));
      
      return View(mixins);
    }

    [HttpGet]
    public IActionResult Delete(){
      return View();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id){
      var mixin = _context.Mixin.FirstOrDefault(mixin => mixin.Id == id);

      if(mixin is null){
        return View();
      }
      else{
        _context.Mixin.Remove(mixin);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(HomeController.Home), "Home");
      }
    }

    [HttpGet]
    public IActionResult Details(string id){
      var mixin = _context.Mixin.FirstOrDefault(mixin => mixin.Id == id);
      
      if(mixin is null){
        return View();
      }
      else{
        return View(mixin);
      }

    }

    // [HttpPut]
    // public async Task<IActionResult> AddTo(){
    //   return View();
    // }

    // [HttpPut]
    // public async Task<IActionResult> Edit(){
    //   return View();
    // }

}