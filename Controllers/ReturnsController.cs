using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRH.Models;

namespace AppRH.Controllers
{
    public class ReturnsController : Controller
    {
        private readonly AppRHContext _context;

        public ReturnsController(AppRHContext context)
        {
            _context = context;
        }

        // GET: Returns
        public async Task<IActionResult> Index()
        {
            var appRHContext = _context.Return.Include(r => r.Customer).Include(r => r.House);
            return View(await appRHContext.ToListAsync());
        }

        // GET: Returns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return
                .Include(r => r.Customer)
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.ReturnID == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Returns/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName");
            ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName");
            return View();
        }

        // POST: Returns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnID,ReturnDate,CustomerID,CustomerName,CustomerSurname,HouseID,HouseName")] Return @return)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ClienteID = (from a in _context.Rental where a.CustomerID == @return.CustomerID && a.CustomerID == @return.CustomerID select a).SingleOrDefault();
                    if(ClienteID != null)
                    {
                        if(ClienteID.RentalDate < @return.ReturnDate)
                        {
                            var House = (from a in _context.House where a.HouseID == @return.HouseID select a).SingleOrDefault();
                            var Cliente = (from a in _context.Customer where a.CustomerID == @return.CustomerID select a).SingleOrDefault();

                            @return.HouseName = House.HouseName;
                            @return.CustomerName = Cliente.CustomerName + " " + Cliente.CustomerSurname;
                            @return.CustomerID = Cliente.CustomerID;
                            @return.HouseID = House.HouseID;
                            House.EstaAlquilada = false;

                            _context.Add(@return);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (System.Exception ex){
                    var error = ex;
                }
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName", @return.CustomerID);
            ViewData["RentalID"] = new SelectList(_context.Rental, "RentalID", "CasaID", "ClienteID");
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == true && x.IsDeleted == false), "HouseID", "HouseName", @return.HouseID);
            return View(@return);
        }

        // GET: Returns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerDNI", @return.CustomerID);
            ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName", @return.HouseID);
            return View(@return);
        }

        // POST: Returns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnID,ReturnDate,CustomerID,CustomerName,CustomerSurname,HouseID,HouseName")] Return @return)
        {
            if (id != @return.ReturnID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@return);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnExists(@return.ReturnID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerDNI", @return.CustomerID);
            ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName", @return.HouseID);
            return View(@return);
        }

        // GET: Returns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return
                .Include(r => r.Customer)
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.ReturnID == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Return == null)
            {
                return Problem("Entity set 'AppRHContext.Return'  is null.");
            }
            var @return = await _context.Return.FindAsync(id);
            if (@return != null)
            {
                _context.Return.Remove(@return);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnExists(int id)
        {
          return (_context.Return?.Any(e => e.ReturnID == id)).GetValueOrDefault();
        }
    }
}
