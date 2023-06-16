using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using admin_labgrown.Models;

namespace admin_labgrown.Controllers
{
    public class TblCountriesController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblCountriesController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblCountries
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblCountries != null ? 
                          View(await _context.TblCountries.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblCountries'  is null.");
        }

        // GET: TblCountries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCountries == null)
            {
                return NotFound();
            }

            var tblCountry = await _context.TblCountries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCountry == null)
            {
                return NotFound();
            }

            return View(tblCountry);
        }

        // GET: TblCountries/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TblCountries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryName")] TblCountry tblCountry)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var country = _context.TblCountries.ToList();
                foreach (var item in country)
                {
                    if(item.CountryName == tblCountry.CountryName)
                    {
                        ViewBag.Countryerror = "Country is already exist";
                        return View(tblCountry);
                    }
                }
                _context.Add(tblCountry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(tblCountry);
        }

        // GET: TblCountries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCountries == null)
            {
                return NotFound();
            }

            var tblCountry = await _context.TblCountries.FindAsync(id);
            if (tblCountry == null)
            {
                return NotFound();
            }
            return View(tblCountry);
        }

        // POST: TblCountries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryName")] TblCountry tblCountry)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblCountry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCountryExists(tblCountry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblCountry);
        }

        // GET: TblCountries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCountries == null)
            {
                return NotFound();
            }

            var tblCountry = await _context.TblCountries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCountry == null)
            {
                return NotFound();
            }

            return View(tblCountry);
        }

        // POST: TblCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblCountries == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblCountries'  is null.");
            }
            var tblCountry = await _context.TblCountries.FindAsync(id);
            if (tblCountry != null)
            {
                _context.TblCountries.Remove(tblCountry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCountryExists(int id)
        {
          return (_context.TblCountries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
