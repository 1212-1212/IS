using EShop.Domain.DTO;
using EShop.Domain.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<Client> UserManager;
        private readonly SignInManager<Client> SignInManager;

        public UserController(UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult Register()
        {
            UserRegistrationDTO model = new();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDTO request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await UserManager.FindByEmailAsync(request.Email);
                
                if (userCheck == null)
                {
                    var user = new Client
                    {
                        Name = request.Name,
                        Surname = request.Surname,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = request.PhoneNumber,
                        ShoppingCart = new ShoppingCart()
                    };
                    user.ShoppingCart.ClientId = user.Id;
                    user.ShoppingCart.Client = user;
                    var result = await UserManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Any())
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDTO model = new();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await UserManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await UserManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }


    }
}
