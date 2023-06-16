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
    public class TblFluorescencesController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblFluorescencesController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblFluorescences
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblFluorescences != null ? 
                          View(await _context.TblFluorescences.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblFluorescences'  is null.");
        }

        // GET: TblFluorescences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblFluorescences == null)
            {
                return NotFound();
            }

            var tblFluorescence = await _context.TblFluorescences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFluorescence == null)
            {
                return NotFound();
            }

            return View(tblFluorescence);
        }

        // GET: TblFluorescences/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TblFluorescences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fluorescence")] TblFluorescence tblFluorescence)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tblFluorescence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblFluorescence);
        }

        // GET: TblFluorescences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblFluorescences == null)
            {
                return NotFound();
            }

            var tblFluorescence = await _context.TblFluorescences.FindAsync(id);
            if (tblFluorescence == null)
            {
                return NotFound();
            }
            return View(tblFluorescence);
        }

        // POST: TblFluorescences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fluorescence")] TblFluorescence tblFluorescence)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblFluorescence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblFluorescence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFluorescenceExists(tblFluorescence.Id))
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
            return View(tblFluorescence);
        }

        // GET: TblFluorescences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblFluorescences == null)
            {
                return NotFound();
            }

            var tblFluorescence = await _context.TblFluorescences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFluorescence == null)
            {
                return NotFound();
            }

            return View(tblFluorescence);
        }

        // POST: TblFluorescences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblFluorescences == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblFluorescences'  is null.");
            }
            var tblFluorescence = await _context.TblFluorescences.FindAsync(id);
            if (tblFluorescence != null)
            {
                _context.TblFluorescences.Remove(tblFluorescence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblFluorescenceExists(int id)
        {
          return (_context.TblFluorescences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
