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
        private readonly IEmailConfirmationService _emailConfirmationService;

        public HomeController(
            IProductService productUriService, 
            ILogger<HomeController> logger,
            IEmailConfirmationService emailConfirmationService)
        {
            _productUriService = productUriService;
            _logger = logger;
            _emailConfirmationService = emailConfirmationService;
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
                _logger.LogInformation("New user with email: {0} and url {1}", olxSubscribe.Email, olxSubscribe.Url);
                await _emailConfirmationService.SendEmailConfirmation(olxSubscribe.Email);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Url", "Error happened while parsing a price");
            }
            return View("ConfirmAlert");
        }
        public IActionResult ConfirmEmail(string id)
        {
            if (id is null) return View("WrongConfirmationCode");
            if (_emailConfirmationService.VerifyConfirmationCode(id))
            {
                return View("Index");
            }
            else
            {
                return View("WrongConfirmationCode");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}