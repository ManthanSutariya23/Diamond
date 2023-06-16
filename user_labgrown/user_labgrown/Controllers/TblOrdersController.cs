using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using user_labgrown.Models;

namespace user_labgrown.Controllers
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Create", "TblRegisters");
            }
            ViewBag.userid = Int32.Parse(HttpContext.Session.GetString("userid"));
            var labgrowndbContext = _context.TblOrders.Include(t => t.Product).Include(t => t.User).Include(t=>t.Product.ShapeNavigation);
            return View(await labgrowndbContext.ToListAsync());
        }

        // GET: TblOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Product)
                .Include(t => t.User)
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Create", "TblRegisters");
            }
            int userid = Int32.Parse(HttpContext.Session.GetString("userid"));
            int productid = (int)HttpContext.Session.GetInt32("productid");
            var product = _context.TblProducts.Include(t => t.CertificateNavigation).Include(t => t.ColorNavigation).Include(t => t.FluorescenceNavigation).Include(t => t.PurityNavigation).Include(t => t.ShapeNavigation);
            foreach (var item in product.ToList())
            {
                if(item.Id == productid)
                {
                    ViewBag.shap = item.ShapeNavigation.ShapeName;
                    ViewBag.carat = item.Carat;
                    ViewBag.purity = item.PurityNavigation.Purity;
                    ViewBag.certificate = item.CertificateNavigation.CertificateName;
                    ViewBag.color = item.ColorNavigation.Color;
                    ViewBag.fluorescence = item.FluorescenceNavigation.Fluorescence;
                    ViewBag.cut = item.CutGrade;
                    ViewBag.symmetry = item.Symmetry;
                    ViewBag.polish = item.Polish;

                }
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", productid);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", userid);
            return View();
        }

        // POST: TblOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,Status,Amount")] TblOrder tblOrder)
        {
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Create", "TblRegisters");
            }
            //if (ModelState.IsValid)
            //{
            int userid = Int32.Parse(HttpContext.Session.GetString("userid"));
            int productid = (int)HttpContext.Session.GetInt32("productid");
            tblOrder.ProductId = productid;
            tblOrder.UserId = userid;
            tblOrder.Status = 0;
            tblOrder.Amount = 12000;

            _context.Add(tblOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            return View(tblOrder);
        }

        // GET: TblOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            return View(tblOrder);
        }

        // POST: TblOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductId,Status,Amount")] TblOrder tblOrder)
        {
            if (id != tblOrder.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
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
            //}
            ViewData["ProductId"] = new SelectList(_context.TblProducts, "Id", "Id", tblOrder.ProductId);
            ViewData["UserId"] = new SelectList(_context.TblRegisters, "Id", "Id", tblOrder.UserId);
            return View(tblOrder);
        }

        // GET: TblOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Create", "TblRegisters");
            }
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .Include(t => t.Product)
                .Include(t => t.User)
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            if (HttpContext.Session.GetString("userid") == null)
            {
                return RedirectToAction("Create", "TblRegisters");
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
