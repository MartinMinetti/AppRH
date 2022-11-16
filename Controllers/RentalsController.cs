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
            var appRHContext = _context.Rental.Include(r => r.Customer);
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
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == false && x.IsDeleted == false), "HouseID", "HouseName");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,RentalDate,CustomerID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(rental);
                        await _context.SaveChangesAsync();

                        var housesTemp = (from a in _context.RentalDetailTemp select a).ToList();
                        foreach (var item in housesTemp)
                        {
                            var details = new RentalDetail
                            {
                                RentalID = rental.RentalID,
                                HouseID = item.HouseID,
                                HouseName = item.HouseName
                            };
                            _context.RentalDetail.Add(details);
                            _context.SaveChanges();
                        }

                        _context.RentalDetailTemp.RemoveRange(housesTemp);
                        _context.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (System.Exception ex)
                    {
                        transaccion.Rollback();
                        var error = ex;
                    }
                }
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerName", rental.CustomerID);
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == false && x.IsDeleted == false), "HouseID", "HouseName");
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'AppRHContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public JsonResult AddHouseTemp(int HouseID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var house = (from a in _context.House where a.HouseID == HouseID select a).SingleOrDefault();
                    house.EstaAlquilada = true;
                    _context.SaveChanges();

                    var houseTemp = new RentalDetailTemp
                    {
                        HouseID = HouseID,
                        HouseName = house.HouseName
                    };
                    _context.RentalDetailTemp.Add(houseTemp);
                    _context.SaveChanges();

                    transaccion.Commit();
                }    
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == false), "HouseID", "HouseName");

            return Json(resultado);
        }

            public JsonResult CancelRental()
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var rentalTemp = (from a in _context.RentalDetailTemp select a).ToList();
                   
                   foreach (var item in rentalTemp)
                   {
                        var house = (from a in _context.House where a.HouseID == item.HouseID select a).SingleOrDefault();
                        house.EstaAlquilada = false;
                        _context.SaveChanges();
                   }
                   _context.RentalDetailTemp.RemoveRange(rentalTemp);
                   _context.SaveChanges();

                    transaccion.Commit();
                }    
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            return Json(resultado);
        }


        public JsonResult SearchTempHouse()
        {
           
           List<RentalDetailTemp> ListadoHouseTemp = new List<RentalDetailTemp>();

           var rentalDetailTemp = (from a in _context.RentalDetailTemp select a).ToList();
            foreach (var item in rentalDetailTemp)
            {
                ListadoHouseTemp.Add(item);  
            }
            
            return Json(ListadoHouseTemp);
        }



         public JsonResult SearchRentalHouse(int RentalID)
        {
           
           List<RentalDetail> ListadoHouse = new List<RentalDetail>();

           var rentalDetail = (from a in _context.RentalDetail where a.RentalID == RentalID select a).ToList();
            foreach (var item in rentalDetail)
            {
                ListadoHouse.Add(item);  
            }
            
            return Json(ListadoHouse);
        }



           public JsonResult QuitarHouse(int HouseID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                   
                    var house = (from a in _context.House where a.HouseID == HouseID select a).SingleOrDefault();
                    house.EstaAlquilada = false;
                    _context.SaveChanges();
                   


                    var rentalTemp = (from a in _context.RentalDetailTemp where a.HouseID == HouseID select a).SingleOrDefault();
                   _context.RentalDetailTemp.RemoveRange(rentalTemp);
                   _context.SaveChanges();

                    transaccion.Commit();
                }    
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }

            return Json(resultado);
        }


        // public JsonResult OneHouse(int HouseID)
        // {
        //     var resultado = true; 
        //     using (var transaccion = _context.Database.BeginTransaction())
        //     {
        //         try
        //         {
        //             var idhouse = (from a in _context.House where a.HouseID == HouseID select a).ToList();
        //             var rentalTemp = (from a in _context.RentalDetailTemp select a).ToList();

        //             if (idhouse == rentalTemp)
        //             {
                        
        //             }
  
        //         }    
        //         catch (System.Exception)
        //         {
        //             transaccion.Rollback();
        //             resultado = false;
        //         }
        //     }
        //      return Json(resultado);
        // }




        private bool RentalExists(int id)
        {
          return (_context.Rental?.Any(e => e.RentalID == id)).GetValueOrDefault();
        }
    }
}
