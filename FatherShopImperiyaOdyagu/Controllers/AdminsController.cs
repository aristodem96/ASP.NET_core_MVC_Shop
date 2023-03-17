using Microsoft.AspNetCore.Mvc;
using FatherShopImperiyaOdyagu.Models;
using FatherShopImperiyaOdyagu.Data;
using Microsoft.EntityFrameworkCore;

namespace FatherShopImperiyaOdyagu.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ShopContext _context;

        public AdminsController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Admin> admins = _context.Admin.ToList();
            return View(admins);
        }

        //Registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration([Bind("Id, Name, PhoneNumber, Email, Password")] Admin admin)
        {
            if(ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Products");
            }
            return View(admin);
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email, Password")] Admin admin)
        {
            if(ModelState.IsValid)
            {
                Admin? logged = await _context.Admin.Where((a) => a.Email == admin.Email && a.Password == admin.Password).FirstOrDefaultAsync();
                if(logged != null)
                {
                    HttpContext.Session.SetInt32("Logged",(int)logged.Id);
                    return RedirectToAction("Index","Products");
                }
            }

            return View(admin);
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FirstOrDefaultAsync(a=>a.Id == id);

            if(admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if(_context.Admin == null)
            {
                return Problem("Not Found!");
            }
            var admin = await _context.Admin.FindAsync(id);
            if(admin != null)
            {
                _context.Admin.Remove(admin);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Products");
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.Admin == null)
            {
                return NotFound();
            }
            var admin = await _context.Admin.FindAsync(id);
            if(admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, [Bind("Id, Name, PhoneNumber, Email, Password")] Admin admin)
        {
            if(id != admin.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!AdminExists(admin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        //Exists
        private bool AdminExists(int id)
        {
            return (_context.Admin?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
