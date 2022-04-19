using Cowsandbulls.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cowsandbulls.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private Solver solver;
        public HomeController(Solver solver)
        {
            this.solver = solver;
        }

        public ViewResult Index()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult index([FromForm]Solver solver)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("submit",solver);
            }
            return View("index",solver);
        }

        public IActionResult submit(Solver solver)
        {
            var datetime = DateTime.Now.ToString();
            var secretcode = datetime.ElementAt(0).ToString() + datetime.ElementAt(1).ToString() + datetime.ElementAt(11).ToString() + datetime.ElementAt(12).ToString();
            ViewBag.secretcode = secretcode;
            return View(solver);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}