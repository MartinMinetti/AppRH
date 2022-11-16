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
            var appRHContext = _context.Return.Include(r => r.Customer);
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
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == true && x.IsDeleted == false), "HouseID", "HouseName");
            return View();
        }

        // POST: Returns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnID,ReturnDate,CustomerID")] Return Return)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(Return);
                        await _context.SaveChangesAsync();

                        var housesTemp = (from a in _context.ReturnDetailTemp select a).ToList();
                        foreach (var item in housesTemp)
                        {
                            var details = new ReturnDetail
                            {
                                ReturnID = Return.ReturnID,
                                HouseID = item.HouseID,
                                HouseName = item.HouseName
                            };
                            _context.ReturnDetail.Add(details);
                            _context.SaveChanges();
                        }

                        _context.ReturnDetailTemp.RemoveRange(housesTemp);
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerDNI", Return.CustomerID);
            ViewData["HouseID"] = new SelectList(_context.House, "HouseID", "HouseName");
            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == true && x.IsDeleted == false), "HouseID", "HouseName");
            return View(Return);
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
                .FirstOrDefaultAsync(m => m.ReturnID == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        public JsonResult AddHouseReturn(int HouseID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var house = (from a in _context.House where a.HouseID == HouseID select a).SingleOrDefault();
                    house.EstaAlquilada = false;
                    _context.SaveChanges();

                    var houseTemp = new ReturnDetailTemp
                    {
                        HouseID = HouseID,
                        HouseName = house.HouseName
                    };
                    _context.ReturnDetailTemp.Add(houseTemp);
                    _context.SaveChanges();

                    transaccion.Commit();
                }    
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }


            ViewData["HouseID"] = new SelectList(_context.House.Where(x => x.EstaAlquilada == true), "HouseID", "HouseName");

            return Json(resultado);
        }

        public JsonResult CancelReturn()
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var returnTemp = (from a in _context.ReturnDetailTemp select a).ToList();
                   
                   foreach (var item in returnTemp)
                   {
                        var house = (from a in _context.House where a.HouseID == item.HouseID select a).SingleOrDefault();
                        house.EstaAlquilada = true;
                        _context.SaveChanges();
                   }
                   _context.ReturnDetailTemp.RemoveRange(returnTemp);
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


         public JsonResult SearchHouseReturnTemp()
        {
           
           List<ReturnDetailTemp> ListadoHouseReturnTemp = new List<ReturnDetailTemp>();

           var returnDetailTemp = (from a in _context.ReturnDetailTemp select a).ToList();
            foreach (var item in returnDetailTemp)
            {
                ListadoHouseReturnTemp.Add(item);  
            }
            
            return Json(ListadoHouseReturnTemp );
        }


          public JsonResult SearchReturnHouse(int ReturnID)
        {
           
           List<ReturnDetail> ListadoHouseReturn = new List<ReturnDetail>();

           var returnDetail = (from a in _context.ReturnDetail where a.ReturnID == ReturnID select a).ToList();
            foreach (var item in returnDetail)
            {
                ListadoHouseReturn.Add(item);  
            }
            
            return Json(ListadoHouseReturn);
        }


        public JsonResult QuitarHouseReturn(int HouseID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                   
                    var house = (from a in _context.House where a.HouseID == HouseID select a).SingleOrDefault();
                    house.EstaAlquilada = true;
                    _context.SaveChanges();
                   


                    var returnTemp = (from a in _context.ReturnDetailTemp where a.HouseID == HouseID select a).SingleOrDefault();
                   _context.ReturnDetailTemp.RemoveRange(returnTemp);
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




        private bool ReturnExists(int id)
        {
          return (_context.Return?.Any(e => e.ReturnID == id)).GetValueOrDefault();
        }
    }
}
