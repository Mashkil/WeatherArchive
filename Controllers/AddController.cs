using Microsoft.AspNetCore.Mvc;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class AddController : Controller
    {
        private AddService _service;
        public AddController(AddService addService)
        {
            _service = addService;
        }

        public async Task<IActionResult> AddFile(IFormFileCollection uploads)
        {
            foreach (var upload in uploads)
            {
                await _service.AddToDatabase(upload);
            }

            return View();
        }

    }
}