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

    [HttpPost]
    public async Task<IActionResult> Create(Mixin model){
      if(!ModelState.IsValid){
        return View(model);
      }

      var userId = _userManager.GetUserId(User);
      var mixin = new Mixin(){ 
        name = model.name,
        body = model.body,
        AccountId = userId
      };

      await _context.Mixin.AddAsync(mixin);
      return RedirectToAction(nameof(List), "List");
    }

    [HttpGet]
    public IActionResult List(){
      var userId = _userManager.GetUserId(User);
      var mixinList = _context.Mixin.Where(mixin => (
        mixin.AccountId == userId
      ));
      
      return View(mixinList);
    }

    [HttpGet("{id}")]   
    public IActionResult Select(string id){
      return View(ViewBag.id);
    }

    [HttpPut]
    public async Task<IActionResult> AddTo(){
      return View();
    }

    [HttpPut]
    public async Task<IActionResult> Edit(){
      return View();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(){
      return View();
    }
}