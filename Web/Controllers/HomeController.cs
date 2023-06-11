using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Extensions.Mappers;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productUriService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productUriService, ILogger<HomeController> logger)
        {
            _productUriService = productUriService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubscribeAsync(OlxSubscribeModel olxSubscribe)
        {
            if(!ModelState.IsValid) return View("Index");
            try
            {
                await _productUriService.Add(olxSubscribe.ToProductDto());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Url", "Error happened while parsing a price");
            }
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}