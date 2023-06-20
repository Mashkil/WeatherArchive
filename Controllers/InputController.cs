using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
    public class InputController : Controller
    {
        public IActionResult Input() { return View(); }

        public IActionResult InputValues(string month, string year)
        {
            return RedirectToAction("ShowArchive", "ShowArchive", new { month, year });
        }
    }
}
