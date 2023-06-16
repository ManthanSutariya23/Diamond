using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using user_labgrown.Models;

namespace user_labgrown.Controllers
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            var labgrowndbContext = _context.TblRegisters.Include(t => t.Country);
            return View(await labgrowndbContext.ToListAsync());
        }

        // GET: TblRegisters/Details/5
        public async Task<IActionResult> Details()
        {
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            String userid = HttpContext.Session.GetString("userid");
            if (userid == null || _context.TblRegisters == null)
            {
                return NotFound();
            }

            var tblRegister = await _context.TblRegisters
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == Int32.Parse(userid));
            if (tblRegister == null)
            {
                return NotFound();
            }

            return View(tblRegister);
        }

        // GET: TblRegisters/Create
        public IActionResult Create()
        {
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
            //if (ModelState.IsValid)
            //{
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
            }
            _context.Add(tblRegister);
                await _context.SaveChangesAsync();
                var labgrowndbContext = _context.TblRegisters;
                var data = labgrowndbContext.ToList();
                String id = "";
                foreach (var item in labgrowndbContext)
                {
                    id = item.Id.ToString();
                }
                HttpContext.Session.SetString("userid", id);
                return RedirectToAction("Index", "Home");


            //}
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TblRegister tblRegister)
        {
            String passerror = "Password is not correct";
            String emailerror = "Email is not correct";
            if (_context.TblRegisters.Any(x => x.Email == tblRegister.Email) && _context.TblRegisters.Any(x => x.Password == tblRegister.Password))
            {
                var labgrowndbContext = _context.TblRegisters;
                var data = labgrowndbContext.ToList();
                String id = "";
                foreach (var item in data)
                {
                    if(tblRegister.Email == item.Email)
                    {
                        id = item.Id.ToString();
                    }
                }
                HttpContext.Session.SetString("userid", id);
                Console.WriteLine(HttpContext.Session.GetString("userid"));
                return RedirectToAction("Index", "Home");
            }
            else if(!_context.TblRegisters.Any(x => x.Email == tblRegister.Email))
            {
                ViewBag.Error = emailerror; 
                return View(nameof(Create));
            }
            else
            {
                ViewBag.Error = passerror;
                return View(nameof(Create));
            }
                
            return RedirectToAction(nameof(Index));


            //}
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        // GET: TblRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
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
            if (HttpContext.Session.GetString("userid") != null)
            {
                ViewBag.Login = true;
                ViewBag.userid = HttpContext.Session.GetString("userid");
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
                return RedirectToAction("Details", "TblRegisters");
            //}
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "Id", "CountryName", tblRegister.CountryId);
            return View(tblRegister);
        }

        // GET: TblRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
