using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreshFruits.Models;
using FreshFruits.Data;
using FreshFruits.Models.Home;
using FreshFruits.Services;
using FreshFruits.Repositories.Interfaces;

namespace FreshFruits.Controllers
{
    public class HomeController : Controller
    {
        private IFruitRepository _fruitRepository;
        private SessionManager _sessionManager;

        public HomeController(IFruitRepository fruitRepository, SessionManager sessionManager)
        {
            _fruitRepository = fruitRepository;
            _sessionManager = sessionManager;
        }

        public IActionResult Index(string sortBy)
        {
            List<Fruit> fruits = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                // Sort by is given...
                switch (sortBy)
                {
                    case "price":
                        fruits = _fruitRepository.GetAll().Result
                                    .OrderBy(f => f.Price)
                                    .ToList();
                        break;
                    case "rating":
                        fruits = _fruitRepository.GetAll().Result
                                    .OrderBy(f => f.Rating)
                                    .ToList();
                        break;
                    default:
                        fruits = _fruitRepository.GetAll().Result.ToList();
                        break;
                }
            }
            else
            {
                // No sort by...
                fruits = _fruitRepository.GetAll().Result.ToList();
            }

            var vm = new IndexViewModel
            {
                Fruits = fruits
            };

            var cart = _sessionManager.GetShoppingCart();
            ViewData["ShoppingCart"] = cart;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var fruit = await _fruitRepository.GetById(id);
            var cart = _sessionManager.GetShoppingCart();
            cart.Add(fruit);
            _sessionManager.SaveShoppingCart(cart);

            TempData["Success"] = "Added item to cart: " + fruit.Name;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var fruit = await _fruitRepository.GetById(id);
            var cart = _sessionManager.GetShoppingCart();
            cart.Remove(fruit);
            _sessionManager.SaveShoppingCart(cart);

            TempData["Success"] = "Removed item from cart: " + fruit.Name;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
