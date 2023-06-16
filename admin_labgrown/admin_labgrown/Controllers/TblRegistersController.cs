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
    public class TblRegistersController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblRegistersController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblRegisters
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            var labgrowndbContext = _context.TblRegisters.Include(t => t.Country);
            return View(await labgrowndbContext.ToListAsync());
        }

        // GET: TblRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblRegisters == null)
            {
                return NotFound();
            }

            var tblRegister = await _context.TblRegisters
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblRegister == null)
            {
                return NotFound();
            }

            return View(tblRegister);
        }

        // GET: TblRegisters/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName");
            return View();
        }

        // POST: TblRegisters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FName,LName,Email,Password,Address1,Address2,Postcode,City,State,CountryId")] TblRegister tblRegister)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            //if (ModelState.IsValid)
            //{
            _context.Add(tblRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        // GET: TblRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblRegisters == null)
            {
                return NotFound();
            }

            var tblRegister = await _context.TblRegisters.FindAsync(id);
            if (tblRegister == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        // POST: TblRegisters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FName,LName,Email,Password,Address1,Address2,Postcode,City,State,CountryId")] TblRegister tblRegister)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblRegister.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tblRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRegisterExists(tblRegister.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        // GET: TblRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblRegisters == null)
            {
                return NotFound();
            }

            var tblRegister = await _context.TblRegisters
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblRegister == null)
            {
                return NotFound();
            }

            return View(tblRegister);
        }

        // POST: TblRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblRegisters == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblRegisters'  is null.");
            }
            var tblRegister = await _context.TblRegisters.FindAsync(id);
            if (tblRegister != null)
            {
                _context.TblRegisters.Remove(tblRegister);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRegisterExists(int id)
        {
          return (_context.TblRegisters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
