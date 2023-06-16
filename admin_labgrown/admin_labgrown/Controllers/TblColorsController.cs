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
    public class TblColorsController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblColorsController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblColors
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblColors != null ? 
                          View(await _context.TblColors.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblColors'  is null.");
        }

        // GET: TblColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblColors == null)
            {
                return NotFound();
            }

            var tblColor = await _context.TblColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblColor == null)
            {
                return NotFound();
            }

            return View(tblColor);
        }

        // GET: TblColors/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TblColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color")] TblColor tblColor)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tblColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblColor);
        }

        // GET: TblColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblColors == null)
            {
                return NotFound();
            }

            var tblColor = await _context.TblColors.FindAsync(id);
            if (tblColor == null)
            {
                return NotFound();
            }
            return View(tblColor);
        }

        // POST: TblColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color")] TblColor tblColor)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblColorExists(tblColor.Id))
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
            return View(tblColor);
        }

        // GET: TblColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblColors == null)
            {
                return NotFound();
            }

            var tblColor = await _context.TblColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblColor == null)
            {
                return NotFound();
            }

            return View(tblColor);
        }

        // POST: TblColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblColors == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblColors'  is null.");
            }
            var tblColor = await _context.TblColors.FindAsync(id);
            if (tblColor != null)
            {
                _context.TblColors.Remove(tblColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblColorExists(int id)
        {
          return (_context.TblColors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
