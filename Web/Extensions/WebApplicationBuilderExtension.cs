using BLL.Services;
using BLL.Services.Interfaces;
using Resources;

namespace Web.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<IOlxService,OlxService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPriceCheckerService, PriceCheckerService>();
            services.AddTransient<IEmailService, EmailService>(emailService => 
                new EmailService(EmailSettings.SmptServer,EmailSettings.Username,EmailSettings.Password,EmailSettings.Email));
            return services;
        }
    }
}
