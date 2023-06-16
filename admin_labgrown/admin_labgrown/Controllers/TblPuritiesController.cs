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
    public class TblPuritiesController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblPuritiesController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblPurities
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblPurities != null ? 
                          View(await _context.TblPurities.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblPurities'  is null.");
        }

        // GET: TblPurities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblPurities == null)
            {
                return NotFound();
            }

            var tblPurity = await _context.TblPurities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPurity == null)
            {
                return NotFound();
            }

            return View(tblPurity);
        }

        // GET: TblPurities/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TblPurities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Purity")] TblPurity tblPurity)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tblPurity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblPurity);
        }

        // GET: TblPurities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblPurities == null)
            {
                return NotFound();
            }

            var tblPurity = await _context.TblPurities.FindAsync(id);
            if (tblPurity == null)
            {
                return NotFound();
            }
            return View(tblPurity);
        }

        // POST: TblPurities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Purity")] TblPurity tblPurity)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblPurity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPurity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPurityExists(tblPurity.Id))
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
            return View(tblPurity);
        }

        // GET: TblPurities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblPurities == null)
            {
                return NotFound();
            }

            var tblPurity = await _context.TblPurities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPurity == null)
            {
                return NotFound();
            }

            return View(tblPurity);
        }

        // POST: TblPurities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblPurities == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblPurities'  is null.");
            }
            var tblPurity = await _context.TblPurities.FindAsync(id);
            if (tblPurity != null)
            {
                _context.TblPurities.Remove(tblPurity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPurityExists(int id)
        {
          return (_context.TblPurities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
