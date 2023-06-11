using BLL.Services.Interfaces;
using HtmlAgilityPack;

namespace BLL.Services
{
    public class OlxService : IOlxService
    {
        public async Task<string> ParsePrice(string url)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync(url);
            responce.EnsureSuccessStatusCode();
            var htmlResult = await responce.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlResult);
            var priceParent = doc.DocumentNode.SelectSingleNode(@"//div[@data-testid=""ad-price-container""]");
            var price = priceParent.InnerText;
            return price;
        }
    }
}
