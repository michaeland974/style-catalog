using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using style_catalog.Models;
using style_catalog.Data;

namespace style_catalog.Controllers;

[Route("Home/[action]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _context;

    public HomeController(ILogger<HomeController> logger, 
                          DatabaseContext context){
        _logger = logger;
        _context = context;
    }

    [Route("~/Home")] 
    public IActionResult Home(){
        return View();
    }

    public IActionResult Browse(){
        var browseAccount = _context.Account.FirstOrDefault(account => 
            account.UserName == "BrowseCollection");
        
        if(browseAccount is not null){
            var mixins = _context.Mixin.Where(mixin => 
                mixin.AccountId == browseAccount.Id);
            return View(mixins);
        }
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
