using BLL.Services.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources;

namespace BLL.Services
{
    public class PriceCheckerService : IPriceCheckerService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly IOlxService _olxService;
        private readonly IEmailService _emailService;
        private readonly ILogger<PriceCheckerService> _logger;

        public PriceCheckerService(
            IDbContextFactory<DataContext> dbContextFactory,
            IOlxService olxService,
            IEmailService emailService, ILogger<PriceCheckerService> logger)
        {
            _dbContextFactory = dbContextFactory;
            _olxService = olxService;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task Start()
        {
            Thread.Sleep(PriceCheckerSettings.DelayInMilliseconds);
            while (true)
            {
                using (var _context = _dbContextFactory.CreateDbContext())
                {
                    var products = _context.Products;
                    foreach (var product in products)
                    {
                        try
                        {
                            var currentPrice = await _olxService.ParsePrice(product.Url);
                            if (product.LastPrice != currentPrice)
                            {
                                _logger.LogInformation("The price on product: {0} has changed from {1} to {2}", product.Url, product.LastPrice, currentPrice);
                                _context.Products.Where(p => p.Url == product.Url).First().LastPrice = currentPrice;
                                await _context.SaveChangesAsync();
                                foreach (var email in product.Emails)
                                {
                                    if(email.IsConfirmed)
                                        await _emailService.SendEmailAsync(email.EmailAddress, "The price has changed", $"The price for product {product.Url} has changed from {product.LastPrice} to {currentPrice}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Source);
                            _logger.LogError(ex.Message);

                        }
                    }
                }
                _logger.LogInformation("Checking has completed");
            }
        }
    }
}
