using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using style_catalog.Models;
using style_catalog.Data;

namespace style_catalog.Controllers
{
  [Route("Account/[action]")]
  public class AccountController : Controller{
    private readonly AccountContext _context;
    public AccountController(AccountContext context){
        _context = context;
    }

    private bool AccountExists(int id){
        return (_context.Account?.Any(e => e.id == id)).GetValueOrDefault();
    }

    // GET: Account
    public async Task<IActionResult> Index(){
        return _context.Account != null ? 
            View(await _context.Account.ToListAsync()) :
            Problem("Entity set 'AccountContext.Account'  is null.");
    }

    // GET: Account/Details/5
    public async Task<IActionResult> Details(int? id){
        if (id == null || _context.Account == null){
            return NotFound();
        }
        var Account = await _context.Account.FirstOrDefaultAsync(m => m.id == id);
        if (Account == null){
            return NotFound();
        }
        return View(Account);
    }

    // GET: Account/Create
    public IActionResult Create(){
        return View();
    }

    // POST: Account/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,Accountname,Role,PasswordHash")] Account Account){
        if (ModelState.IsValid){
            _context.Add(Account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(Account);
    }

    // GET: Account/Edit/5
    public async Task<IActionResult> Edit(int? id){
        if (id == null || _context.Account == null){
            return NotFound();
        }
        var Account = await _context.Account.FindAsync(id);
        if (Account == null){
            return NotFound();
        }
        return View(Account);
    }

    // POST: Account/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Accountname,Role,PasswordHash")] Account Account){
        if (id != Account.id){
            return NotFound();
        }

        if (ModelState.IsValid){
            try{
                _context.Update(Account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!AccountExists(Account.id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(Account);
    }

    // GET: Account/Delete/5
    public async Task<IActionResult> Delete(int? id){
        if (id == null || _context.Account == null){
            return NotFound();
        }
        var Account = await _context.Account.FirstOrDefaultAsync(m => m.id == id);
        if (Account == null){
            return NotFound();
        }
        return View(Account);
    }

    // POST: Account/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id){
        if (_context.Account == null){
            return Problem("Entity set 'AccountContext.Account' is null.");
        }
        var Account = await _context.Account.FindAsync(id);
        if (Account != null){
             _context.Account.Remove(Account);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
  }
}
