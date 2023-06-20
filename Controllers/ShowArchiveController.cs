using Microsoft.AspNetCore.Mvc;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class ShowArchiveController : Controller
    {
        private readonly ShowService _service;
        public ShowArchiveController(ShowService service)
        {
            _service = service;
        }

        public async Task<IActionResult> ShowArchive(string month, string year)
        {
            var testView = await _service.GetData(int.Parse(year), int.Parse(month));

            return View(testView);
        }
    }
}