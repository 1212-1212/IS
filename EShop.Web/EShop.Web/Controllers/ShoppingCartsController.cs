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
using EShop.Service.Implementation;
using System.Security.Claims;
using Stripe;

namespace EShop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICarPartService _carPartService;
        private readonly IShoppingCartContainsCarPartService _shoppingCartContainsCarPartService;
        private readonly IShoppingCartService _shoppingCartService;


        public ShoppingCartsController(IUserService userService, ICarPartService carPartService, IShoppingCartContainsCarPartService shoppingCartContainsCarPartService, IShoppingCartService shoppingCartService)
        { 
            _userService = userService;
            _carPartService = carPartService;
            _shoppingCartService= shoppingCartService;
            _shoppingCartContainsCarPartService = shoppingCartContainsCarPartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(client.ShoppingCart);
        }

     
        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid carPartId, int quantity)
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shoppingCart = client.ShoppingCart;
            var itemInShoppingCart = _shoppingCartContainsCarPartService
                .GetAllFromShoppingCart(shoppingCart)
                .Where(x => x.CarPartId == carPartId)
                .FirstOrDefault();

            _shoppingCartService.SetQuantity(shoppingCart, itemInShoppingCart.CarPart, quantity);


            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Delete(Guid carPartId)
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shoppingCart = client.ShoppingCart;
            var itemInShoppingCart = _shoppingCartContainsCarPartService
                .GetAllFromShoppingCart(shoppingCart)
                .Where(x => x.CarPartId == carPartId)
                .FirstOrDefault();

            _shoppingCartContainsCarPartService.Delete(itemInShoppingCart);
           // _shoppingCartService.

            return RedirectToAction(nameof(Index));
        }

        public Order Order()
        {
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _shoppingCartService.Order(client);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult MakePayment(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var client = _userService.Get(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(Order().totalPrice()) * 100,
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            
           return RedirectToAction(nameof(Index));
            
            
        }


    }
}
