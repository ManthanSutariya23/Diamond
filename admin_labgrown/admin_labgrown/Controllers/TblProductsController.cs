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
    public class TblProductsController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblProductsController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblProducts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            var labgrowndbContext = _context.TblProducts.Include(t => t.CertificateNavigation).Include(t => t.ColorNavigation).Include(t => t.FluorescenceNavigation).Include(t => t.PurityNavigation).Include(t => t.ShapeNavigation);
            return View(await labgrowndbContext.ToListAsync());
        }

        // GET: TblProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (HttpContext.Session.GetString("login") != "1")
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts
                .Include(t => t.CertificateNavigation)
                .Include(t => t.ColorNavigation)
                .Include(t => t.FluorescenceNavigation)
                .Include(t => t.PurityNavigation)
                .Include(t => t.ShapeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return View(tblProduct);
        }

        // GET: TblProducts/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["Certificate"] = new SelectList(_context.TblCertificates, "Id", "CertificateName");
            ViewData["Color"] = new SelectList(_context.TblColors, "Id", "Color");
            ViewData["Fluorescence"] = new SelectList(_context.TblFluorescences, "Id", "Fluorescence");
            ViewData["Purity"] = new SelectList(_context.TblPurities, "Id", "Purity");
            ViewData["Shape"] = new SelectList(_context.TblShapes, "Id", "ShapeName");
            return View();
        }

        // POST: TblProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Shape,Color,Purity,Fluorescence,Certificate,CutGrade,Polish,Symmetry,Carat")] TblProduct tblProduct)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            //if (ModelState.IsValid)
            //{
            _context.Add(tblProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["Certificate"] = new SelectList(_context.TblCertificates, "Id", "CertificateName", tblProduct.Certificate);
            ViewData["Color"] = new SelectList(_context.TblColors, "Id", "Color", tblProduct.Color);
            ViewData["Fluorescence"] = new SelectList(_context.TblFluorescences, "Id", "Fluorescence", tblProduct.Fluorescence);
            ViewData["Purity"] = new SelectList(_context.TblPurities, "Id", "Purity", tblProduct.Purity);
            ViewData["Shape"] = new SelectList(_context.TblShapes, "Id", "ShapeName", tblProduct.Shape);
            return View(tblProduct);
        }

        // GET: TblProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }
            ViewData["Certificate"] = new SelectList(_context.TblCertificates, "Id", "CertificateName", tblProduct.Certificate);
            ViewData["Color"] = new SelectList(_context.TblColors, "Id", "Color", tblProduct.Color);
            ViewData["Fluorescence"] = new SelectList(_context.TblFluorescences, "Id", "Fluorescence", tblProduct.Fluorescence);
            ViewData["Purity"] = new SelectList(_context.TblPurities, "Id", "Purity", tblProduct.Purity);
            ViewData["Shape"] = new SelectList(_context.TblShapes, "Id", "ShapeName", tblProduct.Shape);
            return View(tblProduct);
        }

        // POST: TblProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Shape,Color,Purity,Fluorescence,Certificate,CutGrade,Polish,Symmetry,Carat")] TblProduct tblProduct)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblProduct.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tblProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblProductExists(tblProduct.Id))
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
            ViewData["Certificate"] = new SelectList(_context.TblCertificates, "Id", "Id", tblProduct.Certificate);
            ViewData["Color"] = new SelectList(_context.TblColors, "Id", "Id", tblProduct.Color);
            ViewData["Fluorescence"] = new SelectList(_context.TblFluorescences, "Id", "Id", tblProduct.Fluorescence);
            ViewData["Purity"] = new SelectList(_context.TblPurities, "Id", "Id", tblProduct.Purity);
            ViewData["Shape"] = new SelectList(_context.TblShapes, "Id", "Id", tblProduct.Shape);
            return View(tblProduct);
        }

        // GET: TblProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblProducts == null)
            {
                return NotFound();
            }

            var tblProduct = await _context.TblProducts
                .Include(t => t.CertificateNavigation)
                .Include(t => t.ColorNavigation)
                .Include(t => t.FluorescenceNavigation)
                .Include(t => t.PurityNavigation)
                .Include(t => t.ShapeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            return View(tblProduct);
        }

        // POST: TblProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblProducts == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblProducts'  is null.");
            }
            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct != null)
            {
                _context.TblProducts.Remove(tblProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblProductExists(int id)
        {
          return (_context.TblProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
