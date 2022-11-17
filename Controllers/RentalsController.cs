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
    public class RentalsController : Controller
    {
        private readonly AppRHContext _context;

        public RentalsController(AppRHContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var appRHContext = _context.Rental.Include(r => r.Customer).Include(r => r.House);
            return View(await appRHContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Customer)
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName");
            // ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName");
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == false && x.IsDeleted == false), "HouseID", "HouseName");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,RentalDate,CustomerID,CustomerName,CustomerSurname,HouseID,HouseName")] Rental rental)
        {
            if (ModelState.IsValid)
            {   
                var House = (from a in _context.House where a.HouseID == rental.HouseID select a).SingleOrDefault();
                var Cliente = (from a in _context.Customer where a.CustomerID == rental.CustomerID select a).SingleOrDefault();


                rental.HouseName = House.HouseName;
                rental.CustomerName = Cliente.CustomerName + " " + Cliente.CustomerSurname;
                rental.CustomerID = Cliente.CustomerID;
                rental.HouseID = House.HouseID;
                House.EstaAlquilada = true;

                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName", rental.CustomerID);
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == false && x.IsDeleted == false), "HouseID", "HouseName", rental.HouseID);
            return View(rental);
        }



        private bool RentalExists(int id)
        {
          return (_context.Rental?.Any(e => e.RentalID == id)).GetValueOrDefault();
        }
    }
}
