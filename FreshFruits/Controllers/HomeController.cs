using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreshFruits.Models;
using FreshFruits.Data;
using FreshFruits.Models.Home;

namespace FreshFruits.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                        fruits = _dbContext.Fruits
                                    .OrderBy(f => f.Price)
                                    .ToList();
                        break;
                    case "rating":
                        fruits = _dbContext.Fruits
                                    .OrderBy(f => f.Rating)
                                    .ToList();
                        break;
                    default:
                        fruits = _dbContext.Fruits.ToList();
                        break;
                }
            }
            else
            {
                // No sort by...
                fruits = _dbContext.Fruits.ToList();
            }

            var vm = new IndexViewModel
            {
                Fruits = fruits
            };

            return View(vm);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
