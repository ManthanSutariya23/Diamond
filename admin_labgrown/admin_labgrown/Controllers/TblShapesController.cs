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
    public class TblShapesController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblShapesController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblShapes
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblShapes != null ? 
                          View(await _context.TblShapes.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblShapes'  is null.");
        }

        // GET: TblShapes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblShapes == null)
            {
                return NotFound();
            }

            var tblShape = await _context.TblShapes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblShape == null)
            {
                return NotFound();
            }

            return View(tblShape);
        }

        // GET: TblShapes/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TblShapes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShapeName")] TblShape tblShape)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tblShape);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblShape);
        }

        // GET: TblShapes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblShapes == null)
            {
                return NotFound();
            }

            var tblShape = await _context.TblShapes.FindAsync(id);
            if (tblShape == null)
            {
                return NotFound();
            }
            return View(tblShape);
        }

        // POST: TblShapes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShapeName")] TblShape tblShape)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblShape.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblShape);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblShapeExists(tblShape.Id))
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
            return View(tblShape);
        }

        // GET: TblShapes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblShapes == null)
            {
                return NotFound();
            }

            var tblShape = await _context.TblShapes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblShape == null)
            {
                return NotFound();
            }

            return View(tblShape);
        }

        // POST: TblShapes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblShapes == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblShapes'  is null.");
            }
            var tblShape = await _context.TblShapes.FindAsync(id);
            if (tblShape != null)
            {
                _context.TblShapes.Remove(tblShape);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblShapeExists(int id)
        {
          return (_context.TblShapes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
