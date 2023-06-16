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
    public class TblCertificatesController : Controller
    {
        private readonly LabgrowndbContext _context;

        public TblCertificatesController(LabgrowndbContext context)
        {
            _context = context;
        }

        // GET: TblCertificates
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.TblCertificates != null ? 
                          View(await _context.TblCertificates.ToListAsync()) :
                          Problem("Entity set 'LabgrowndbContext.TblCertificates'  is null.");
        }

        // GET: TblCertificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCertificates == null)
            {
                return NotFound();
            }

            var tblCertificate = await _context.TblCertificates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCertificate == null)
            {
                return NotFound();
            }

            return View(tblCertificate);
        }

        // GET: TblCertificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblCertificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CertificateName")] TblCertificate tblCertificate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCertificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCertificate);
        }

        // GET: TblCertificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCertificates == null)
            {
                return NotFound();
            }

            var tblCertificate = await _context.TblCertificates.FindAsync(id);
            if (tblCertificate == null)
            {
                return NotFound();
            }
            return View(tblCertificate);
        }

        // POST: TblCertificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CertificateName")] TblCertificate tblCertificate)
        {
            if (id != tblCertificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCertificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCertificateExists(tblCertificate.Id))
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
            return View(tblCertificate);
        }

        // GET: TblCertificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.TblCertificates == null)
            {
                return NotFound();
            }

            var tblCertificate = await _context.TblCertificates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCertificate == null)
            {
                return NotFound();
            }

            return View(tblCertificate);
        }

        // POST: TblCertificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("login") != "1")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_context.TblCertificates == null)
            {
                return Problem("Entity set 'LabgrowndbContext.TblCertificates'  is null.");
            }
            var tblCertificate = await _context.TblCertificates.FindAsync(id);
            if (tblCertificate != null)
            {
                _context.TblCertificates.Remove(tblCertificate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCertificateExists(int id)
        {
          return (_context.TblCertificates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
