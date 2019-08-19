using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNdishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNdishes.Controllers
{
    public class HomeController : Controller
    {
        private Mycontext DbContext;

        public HomeController(Mycontext context)
        {
            DbContext = context;
        }

        public IActionResult Index()
        {
            var chefs = DbContext.Chefs
                        .Include(c => c.CreatedDishes)
                        .ToList();

            foreach(var chef in chefs)
            {
                TimeSpan age = DateTime.Now - chef.DOB;
                TimeSpan years = age/365;
                ViewBag.Age = years;
            } 
            return View(chefs);
        }

        [HttpGet("new")]
        public IActionResult newChef()
        {
            return View();
        }

        [HttpPost("newchef")]
        public IActionResult AddChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {   
                DbContext.Add(newChef);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                return View("newChef");
            }
        }

        [HttpGet("/dishes")]
        public IActionResult AllDishes()
        {
            var dishes = DbContext.Dishes
                .Include(c => c.Creator)
                .ToList();
            return View(dishes);
        }

        [HttpGet("dishes/new")]
        public IActionResult newDish()
        {
            ViewBag.chefs = DbContext.Chefs.ToList();
            return View();
        }

        [HttpPost("addDish")]
        public IActionResult addDish(Dish newDish)
        {
            ViewBag.chefs = DbContext.Chefs.ToList();
            if(ModelState.IsValid)
            {
                DbContext.Add(newDish);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("newDish");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
