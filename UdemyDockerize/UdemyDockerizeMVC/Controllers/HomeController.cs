using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UdemyDockerizeMVC.Models;

namespace UdemyDockerizeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileProvider _provider;
        public HomeController(ILogger<HomeController> logger, IFileProvider provider)
        {
            _provider = provider;
        }


        public IActionResult Index()
        {
            var images = _provider.GetDirectoryContents("wwwroot/images")
                        .ToList()
                        .Select(x => x.Name);
            return View(images);
        }

        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ImageDelete(string name)
        {
            var file = _provider.GetDirectoryContents("wwwroot/images")
                .ToList()
                .First(x => x.Name == name);

            System.IO.File.Delete(file.PhysicalPath);

            return RedirectToAction("Index");
        }


    }
}
