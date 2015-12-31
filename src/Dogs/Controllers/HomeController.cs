using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dogs.Models;
using Microsoft.AspNet.Mvc;

namespace Dogs.Controllers
{
    public class HomeController : Controller
    {
        private MyDBContext context;

        public HomeController(MyDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            context.Dogs.Add(new Dog() { Name = "puppy" });

            context.SaveChanges();
            ViewBag.Dogs = context.Dogs;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
