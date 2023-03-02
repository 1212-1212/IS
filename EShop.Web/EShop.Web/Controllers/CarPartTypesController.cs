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
    public class CarPartTypesController : Controller
    {
        private readonly ICarPartTypeService _context;

        public CarPartTypesController(ICarPartTypeService context)
        {
            _context = context;
        }

        // GET: CarPartTypes
        public async Task<IActionResult> Index()
        {
              return View(_context.GetAll());
        }

        // GET: CarPartTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Get(id) == null)
            {
                return NotFound();
            }

            var carPartType = _context.Get(id);
            if (carPartType == null)
            {
                return NotFound();
            }

            return View(carPartType);
        }

        // GET: CarPartTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPartTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Id")] CarPartType carPartType)
        {
             carPartType.Id = Guid.NewGuid();
                _context.Create(carPartType);
                return RedirectToAction(nameof(Index));
           
        }

        // GET: CarPartTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var carPartType = _context.Get(id);
            if (carPartType == null)
            {
                return NotFound();
            }
            return View(carPartType);
        }

        // POST: CarPartTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Type,Id")] CarPartType carPartType)
        {
            if (id != carPartType.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(carPartType);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartTypeExists(carPartType.Id))
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

        // GET: CarPartTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var carPartType = _context.Get(id);
            if (carPartType == null)
            {
                return NotFound();
            }

            return View(carPartType);
        }

        // POST: CarPartTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarPartTypes'  is null.");
            }
            var carPartType = _context.Get(id);
            if (carPartType != null)
            {
                _context.Delete(carPartType.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarPartTypeExists(Guid id)
        {
            return _context.Get(id) != null;
        }
    }
}
