using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRH.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppRH.Controllers
{
    [Authorize]
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
            // ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName");
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == true && x.IsDeleted == false), "HouseID", "HouseName");
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
                    var ClienteID = (from a in _context.Rental where a.CustomerID == @return.CustomerID && a.HouseID == @return.HouseID select a).SingleOrDefault();
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


        private bool ReturnExists(int id)
        {
          return (_context.Return?.Any(e => e.ReturnID == id)).GetValueOrDefault();
        }
    }
}
