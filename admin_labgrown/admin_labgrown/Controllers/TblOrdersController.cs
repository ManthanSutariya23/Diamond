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
    public class TblOrdersController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblOrdersController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblOrders
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            var labgrowndbContext = _context.TblOrders.Include(t => t.Product.ShapeNavigation).Include(t => t.User).Include(t => t.Product);
            return View(await labgrowndbContext.ToListAsync());
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }
            tblOrder.Status = 1;
            _context.Update(tblOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TblOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Product)
                .Include(t => t.User)
                .Include(t => t.Product.ShapeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // GET: TblOrders/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id");
            return View();
        }

        // POST: TblOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,Status,Amount")] TblOrder tblOrder)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tblOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            return View(tblOrder);
        }

        // GET: TblOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }
            tblOrder.Status = 1;
            _context.Update(tblOrder);
            //ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            //ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            //return View(tblOrder);
            var labgrowndbContext = _context.TblOrders.Include(t => t.Product.ShapeNavigation).Include(t => t.User).Include(t => t.Product);
            return View("Index",await labgrowndbContext.ToListAsync());
        }

        // POST: TblOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductId,Status,Amount")] TblOrder tblOrder)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != tblOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrderExists(tblOrder.Id))
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
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            return View(tblOrder);
        }

        // GET: TblOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Product)
                .Include(t => t.User)
                .Include(t => t.Product.ShapeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // POST: TblOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblOrders == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblOrders'  is null.");
            }
            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder != null)
            {
                _context.TblOrders.Remove(tblOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderExists(int id)
        {
          return (_context.TblOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
