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
using System.Drawing.Drawing2D;
using EShop.Domain.ViewModels;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class CarPartsController : Controller
    {
        private readonly ICarPartService _service;

        private readonly ICarPartBrandService _brandService;

        private readonly ICarPartStageService _stageService;

        private readonly ICarPartTypeService _typeService;

        private readonly IUserService _userService;

        public CarPartsController(ICarPartService service, ICarPartBrandService brandService, ICarPartStageService stageService, ICarPartTypeService typeService, IUserService userService)
        {
            _service = service;
            _brandService = brandService;
            _stageService = stageService;
            _typeService = typeService;
            _userService = userService;

        }


        // GET: CarParts
        public IActionResult Index(string SearchString, Guid CarPartBrandId, Guid CarPartTypeId, Guid CarPartStageId)
        {
            ViewBag.Brands = _brandService.GetAll();
            ViewBag.Types = _typeService.GetAll();
            ViewBag.Stages = _stageService.GetAll();
            var carParts = _service.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {
                carParts = _service.SearchBySearchString(SearchString);
            }
            
            if(!Guid.Empty.Equals(CarPartBrandId))
            {
                carParts = carParts.Intersect(_service.SearchByBrandName(_brandService.Get(CarPartBrandId)));
            }
            if (!Guid.Empty.Equals(CarPartTypeId))
            {
                carParts = carParts.Intersect(_service.SearchByType(_typeService.Get(CarPartTypeId)));
            }
            if (!Guid.Empty.Equals(CarPartStageId))
            {
                carParts = carParts.Intersect(_service.SearchByStage(_stageService.Get(CarPartStageId)));
            }

            var carPartsDto = new CarPartsViewModel
            {
                CarParts = carParts,
                SearchString = SearchString,
                CarPartBrandId = CarPartBrandId,
                CarPartTypeId = CarPartTypeId,
                CarPartStageId = CarPartStageId,
        };
              
                
            return View(carPartsDto);
        }

        // GET: CarParts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _service.Get(id) == null)
            {
                return NotFound();
            }

         
            var carPart = _service.Get(id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // GET: CarParts/Create
        public IActionResult Create()
        {
            ViewData["CarPartBrandId"] = new SelectList(_brandService.GetAll(), "Id", "BrandName");
            ViewData["CarPartStageId"] = new SelectList(_stageService.GetAll(), "Id", "Stage");
            ViewData["CarPartTypeId"] = new SelectList(_typeService.GetAll(), "Id", "Type");
            return View();
        }

        // POST: CarParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarPartTypeId,CarPartStageId,Description,CarPartBrandId,Id,Price")] CarPart carPart)
        {
           
              carPart.Id = Guid.NewGuid();

            var entity = new CarPart
            {
                CarPartTypeId = carPart.CarPartTypeId,
                Type = _typeService.Get(carPart.CarPartTypeId),
                CarPartStageId = carPart.CarPartStageId,
                Stage = _stageService.Get(carPart.CarPartStageId),
                Description = carPart.Description,
                CarPartBrandId = carPart.CarPartBrandId,
                Brand = _brandService.Get(carPart.CarPartBrandId),
                Price = carPart.Price,
            };

            _service.Create(entity);

            ViewData["CarPartBrandId"] = new SelectList(_brandService.GetAll(), "Id", "BrandName");
            ViewData["CarPartStageId"] = new SelectList(_stageService.GetAll(), "Id", "Stage");
            ViewData["CarPartTypeId"] = new SelectList(_typeService.GetAll(), "Id", "Type");
            return RedirectToAction(nameof(Index));
        }

        // GET: CarParts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _service.Get(id) == null)
            {
                return NotFound();
            }


            var carPart = _service.Get(id);

            if (carPart == null)
            {
                return NotFound();
            }
            ViewData["CarPartBrandId"] = new SelectList(_brandService.GetAll(), "Id", "BrandName");
            ViewData["CarPartStageId"] = new SelectList(_stageService.GetAll(), "Id", "Stage");
            ViewData["CarPartTypeId"] = new SelectList(_typeService.GetAll(), "Id", "Type");
            return View(carPart);
        }

        // POST: CarParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarPartTypeId,CarPartStageId,Description,CarPartBrandId,Id,Price")] CarPart carPart)
        {
            if (id != carPart.Id)
            {
                return NotFound();
            }

           
                try
                {
                var existing = _service.Get(carPart.Id);

                existing.CarPartTypeId = carPart.CarPartTypeId;
                existing.CarPartBrandId = carPart.CarPartBrandId;
                existing.CarPartStageId = carPart.CarPartStageId;
                existing.Description = carPart.Description;
                existing.Price = carPart.Price;
                var entity = new CarPart
                {
                    CarPartTypeId = carPart.CarPartTypeId,
                    Type = _typeService.Get(carPart.CarPartTypeId),
                    CarPartStageId = carPart.CarPartStageId,
                    Stage = _stageService.Get(carPart.CarPartStageId),
                    Description = carPart.Description,
                    CarPartBrandId = carPart.CarPartBrandId,
                    Brand = _brandService.Get(carPart.CarPartBrandId),
                    Price = carPart.Price,
                };
                _service.Update(existing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartExists(carPart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            ViewData["CarPartBrandId"] = new SelectList(_brandService.GetAll(), "Id", "BrandName");
            ViewData["CarPartStageId"] = new SelectList(_stageService.GetAll(), "Id", "Stage");
            ViewData["CarPartTypeId"] = new SelectList(_typeService.GetAll(), "Id", "Type");
            return RedirectToAction(nameof(Index));
            

        }

        // GET: CarParts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _service.Get(id) == null)
            {
                return NotFound();
            }
         
            var carPart = _service.Get(id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // POST: CarParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_service.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarParts'  is null.");
            }
            var carPart = _service.Get(id);
            if (carPart != null)
            {
                _service.Delete(carPart.Id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool CarPartExists(Guid id)
        {
          return _service.Get(id) != null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToShoppingCart(Guid carPartId)
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var carPart = _service.Get(carPartId);

            _service.AddToShoppingCart(carPart, client.ShoppingCart);

            return RedirectToAction(nameof(Index));
        }

      
    }
}
