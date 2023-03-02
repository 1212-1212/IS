using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Models;
using Eshop.Repository.Data;
using EShop.Service.Interface;

namespace EShop.Web.Controllers
{
    public class CarPartBrandsController : Controller
    {
        private readonly ICarPartBrandService _context;

        public CarPartBrandsController(ICarPartBrandService context)
        {
            _context = context;
        }

        // GET: CarPartBrands
        public async Task<IActionResult> Index()
        {
              return View( _context.GetAll());
        }

        // GET: CarPartBrands/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Get(id) == null)
            {
                return NotFound();
            }

            var carPartBrand = _context.Get(id);
            if (carPartBrand == null)
            {
                return NotFound();
            }

            return View(carPartBrand);
        }

        // GET: CarPartBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPartBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandName,Country,Id")] CarPartBrand carPartBrand)
        {
            
                carPartBrand.Id = Guid.NewGuid();
            _context.Create(carPartBrand);
                return RedirectToAction(nameof(Index));
            
            return View(carPartBrand);
        }

        // GET: CarPartBrands/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Get(id) == null)
            {
                return NotFound();
            }

            var carPartBrand = _context.Get(id);
            if (carPartBrand == null)
            {
                return NotFound();
            }
            return View(carPartBrand);
        }

        // POST: CarPartBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BrandName,Country,Id")] CarPartBrand carPartBrand)
        {
            if (id != carPartBrand.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(carPartBrand);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartBrandExists(carPartBrand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(carPartBrand);
        }

        // GET: CarPartBrands/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var carPartBrand = _context.Get(id);
            if (carPartBrand == null)
            {
                return NotFound();
            }

            return View(carPartBrand);
        }

        // POST: CarPartBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarPartBrands'  is null.");
            }
            var carPartBrand = _context.Get(id);
            if (carPartBrand != null)
            {
                _context.Delete(carPartBrand.Id);
            }
            
      
            return RedirectToAction(nameof(Index));
        }

        private bool CarPartBrandExists(Guid id)
        {
            return _context.Get(id) != null;
        }
    }
}
