using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreshFruits.Data;
using FreshFruits.Models;
using Microsoft.AspNetCore.Authorization;
using FreshFruits.Repositories.Interfaces;

namespace FreshFruits.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FruitsController : Controller
    {
        private readonly IFruitRepository _fruitRepository;

        public FruitsController(IFruitRepository fruitRepository)
        {
            _fruitRepository = fruitRepository;
        }

        // GET: Admin/Fruits
        public async Task<IActionResult> Index()
        {
            return View(await _fruitRepository.GetAll());
        }

        // GET: Admin/Fruits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruit = await _fruitRepository.GetById(id.Value);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // GET: Admin/Fruits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Fruits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fruit fruit)
        {
            if (ModelState.IsValid && Validate(fruit))
            {
                await _fruitRepository.Add(fruit);
                return RedirectToAction(nameof(Index));
            }
            return View(fruit);
        }

        // GET: Admin/Fruits/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruit = await _fruitRepository.GetById(id.Value);
            if (fruit == null)
            {
                return NotFound();
            }
            return View(fruit);
        }

        // POST: Admin/Fruits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && Validate(fruit))
            {
                await _fruitRepository.Update(fruit);
                return RedirectToAction(nameof(Index));
            }
            return View(fruit);
        }

        // GET: Admin/Fruits/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruit = await _fruitRepository.GetById(id.Value);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // POST: Admin/Fruits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fruit = await _fruitRepository.GetById(id);
            if (fruit == null)
            {
                return NotFound();
            }

            await _fruitRepository.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool Validate(Fruit fruit)
        {
            if (string.IsNullOrEmpty(fruit.Name))
                return false;

            return true;
        }
    }
}
