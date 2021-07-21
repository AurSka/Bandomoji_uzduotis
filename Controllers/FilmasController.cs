using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Bandomoji_uzduotis.Data;
using Bandomoji_uzduotis.Models;

namespace Bandomoji_uzduotis.Controllers
{
    public class FilmasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filmas

        public async Task<IActionResult> Index(string searchString, int? pageCount)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CurrentString = searchString;
            }
            
            if (pageCount != null)
            {
                ViewBag.PageNumber = pageCount;
            }
            else if (ViewBag.PageNumber == null)
            {
                ViewBag.PageNumber = 1;
            }
            ViewBag.Count = _context.Filmas.FromSqlRaw("SELECT * FROM Filmas WHERE Pavadinimas LIKE '%" + (string)ViewBag.CurrentString + "%' ").Count();
            return View(await _context.Filmas.FromSqlRaw("SELECT * FROM Filmas WHERE Pavadinimas LIKE '%" + (string)ViewBag.CurrentString + "%' " +
                "ORDER BY Pavadinimas OFFSET " + (10 * ((int)ViewBag.PageNumber - 1) + "ROWS FETCH NEXT 10 ROWS ONLY").ToString()).ToListAsync());
        }

        // GET: Filmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmas = await _context.Filmas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (filmas == null)
            {
                return NotFound();
            }

            return View(filmas);
        }

        // GET: Filmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Pavadinimas,IsleidimoData,FilmoZanras")] Filmas filmas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmas);
        }

        // GET: Filmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmas = await _context.Filmas.FindAsync(id);
            if (filmas == null)
            {
                return NotFound();
            }
            return View(filmas);
        }

        // POST: Filmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Pavadinimas,IsleidimoData,FilmoZanras")] Filmas filmas)
        {
            if (id != filmas.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmasExists(filmas.ID))
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
            return View(filmas);
        }

        // GET: Filmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmas = await _context.Filmas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (filmas == null)
            {
                return NotFound();
            }

            return View(filmas);
        }

        // POST: Filmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmas = await _context.Filmas.FindAsync(id);
            _context.Filmas.Remove(filmas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmasExists(int id)
        {
            return _context.Filmas.Any(e => e.ID == id);
        }
    }
}
