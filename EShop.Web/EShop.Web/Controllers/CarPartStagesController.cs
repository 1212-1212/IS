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
using System.Drawing.Printing;

namespace EShop.Web.Controllers
{
    public class CarPartStagesController : Controller
    {
        private readonly ICarPartStageService _context;

        public CarPartStagesController(ICarPartStageService context)
        {
            _context = context;
        }

        // GET: CarPartStages
        public async Task<IActionResult> Index()
        {
              return View(_context.GetAll());
        }

        // GET: CarPartStages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var carPartStage = _context.Get(id);

            if (carPartStage == null)
            {
                return NotFound();
            }

            return View(carPartStage);
        }

        // GET: CarPartStages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPartStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Stage, Id")] CarPartStage carPartStage)
        {
             carPartStage.Id = Guid.NewGuid();
             _context.Create(carPartStage);
             return RedirectToAction(nameof(Index));
            
        }

        // GET: CarPartStages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            //var carPartStage = await _context.CarPartStages.FindAsync(id);
            var carPartStage = _context.Get(id);
            if (carPartStage == null)
            {
                return NotFound();
            }
            return View(carPartStage);
        }

        // POST: CarPartStages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Stage,Id")] CarPartStage carPartStage)
        {
            if (id != carPartStage.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(carPartStage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartStageExists(carPartStage.Id))
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

        // GET: CarPartStages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var carPartStage = _context.Get(id);
            if (carPartStage == null)
            {
                return NotFound();
            }

            return View(carPartStage);
        }

        // POST: CarPartStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarPartStages'  is null.");
            }
            var carPartStage = _context.Get(id);
            if (carPartStage != null)
            {
                _context.Delete(carPartStage.Id);
            }
            

            return RedirectToAction(nameof(Index));
        }

        private bool CarPartStageExists(Guid id)
        {
            return _context.Get(id) != null;
        }
    }
}
