using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using style_catalog.Data;
using style_catalog.Models;

namespace style_catalog.Controllers
{
  public class UserController : Controller{
    private readonly UserContext _context;
    public UserController(UserContext context){
        _context = context;
    }
     private bool UserExists(int id){
        return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // GET: User
    public async Task<IActionResult> Index(){
        return _context.User != null ? 
            View(await _context.User.ToListAsync()) :
            Problem("Entity set 'UserContext.User'  is null.");
    }

    // GET: User/Details/5
    public async Task<IActionResult> Details(int? id){
        if (id == null || _context.User == null){
            return NotFound();
        }
        var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
        if (user == null){
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public IActionResult Create(){
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,username,Role,PasswordHash")] User user){
        if (ModelState.IsValid){
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public async Task<IActionResult> Edit(int? id){
        if (id == null || _context.User == null){
            return NotFound();
        }
        var user = await _context.User.FindAsync(id);
        if (user == null){
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,username,Role,PasswordHash")] User user){
        if (id != user.Id){
            return NotFound();
        }

        if (ModelState.IsValid){
            try{
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!UserExists(user.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public async Task<IActionResult> Delete(int? id){
        if (id == null || _context.User == null){
            return NotFound();
        }
        var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
        if (user == null){
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id){
        if (_context.User == null){
            return Problem("Entity set 'UserContext.User' is null.");
        }
        var user = await _context.User.FindAsync(id);
        if (user != null){
             _context.User.Remove(user);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
  }
}
