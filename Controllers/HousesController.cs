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
    public class HousesController : Controller
    {
        private readonly AppRHContext _context;

        public HousesController(AppRHContext context)
        {
            _context = context;
        }

        // GET: Houses
        public async Task<IActionResult> Index()
        {
              return _context.House != null ? 
                          View(await _context.House.ToListAsync()) :
                          Problem("Entity set 'AppRHContext.House'  is null.");
        }

        // GET: Houses/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.House == null)
        //     {
        //         return NotFound();
        //     }

        //     var house = await _context.House
        //         .FirstOrDefaultAsync(m => m.HouseID == id);
        //     if (house == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(house);
        // }

        // GET: Houses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Houses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseID,HouseName,HouseAddress,OwnerHouse,PhotoHouse,EstaAlquilada")] House house, IFormFile PhotoHouse)
        {
            if (ModelState.IsValid)
            {
                if (PhotoHouse != null && PhotoHouse.Length > 0)
                {
                    byte[]? Img = null;
                    using (var fs1 = PhotoHouse.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        Img = ms1.ToArray();
                    }
                    house.PhotoHouse = Img;

                }


                    house.HouseName = house.HouseName.ToUpper();
                    house.HouseAddress = house.HouseAddress.ToUpper();
                    house.OwnerHouse = house.OwnerHouse.ToUpper();

                    _context.Add(house);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(house);
        }

        // GET: Houses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.House == null)
            {
                return NotFound();
            }

            var house = await _context.House.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        // POST: Houses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseID,HouseName,HouseAddress,OwnerHouse,PhotoHouse,EstaAlquilada")] House house, IFormFile PhotoHouse)
        {
            if (id != house.HouseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PhotoHouse != null && PhotoHouse.Length > 0)
                    {
                        byte[]? Img = null;
                        using (var fs1 = PhotoHouse.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            Img = ms1.ToArray();
                        }
                        house.PhotoHouse = Img;


                     } 

                        house.HouseName = house.HouseName.ToUpper();
                        house.HouseAddress = house.HouseAddress.ToUpper();
                        house.OwnerHouse = house.OwnerHouse.ToUpper();   



                        _context.Update(house);
                        await _context.SaveChangesAsync();
                   
                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.HouseID))
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
            return View(house);
        }

        // GET: Houses/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null || _context.House == null)
        //     {
        //         return NotFound();
        //     }

        //     var house = await _context.House
        //         .FirstOrDefaultAsync(m => m.HouseID == id);
        //     if (house == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(house);
        // }

        // POST: Houses/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]



        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var house = await _context.House.FindAsync(id);
            if (house != null)
            {
                var houseAlquilada = (from a in _context.Rental where a.HouseID == id select a).ToList();
                if (houseAlquilada.Count == 0)  
                {
                    _context.House.Remove(house);
                    await _context.SaveChangesAsync();
                } 
                else
                {
                    house.IsDeleted = true; 
                    house.HouseName = "Eliminada";
                    _context.Update(house);
                    await _context.SaveChangesAsync();
                }             

            }

            return RedirectToAction(nameof(Index));
        }

        
             
 


        private bool HouseExists(int id)
        {
          return (_context.House?.Any(e => e.HouseID == id)).GetValueOrDefault();
        }
    }
}
