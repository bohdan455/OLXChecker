using BLL.Dto;
using BLL.Extensions.Mappers;
using BLL.Services.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IOlxService _olxService;

        public ProductService(DataContext context, IOlxService olxService)
        {
            _context = context;
            _olxService = olxService;
        }
        public async Task Add(ProductDto productDto)
        {
            var product = _context.Products.Include(p => p.Emails).FirstOrDefault(p => p.Url == productDto.Url);
            if (product is null)
            {
                product = productDto.ToProduct();
                product.LastPrice = await _olxService.ParsePrice(productDto.Url);
                _context.Products.Add(product);

            }
            else
            {
                var email = new Email { EmailAddress = productDto.Email };
                product.Emails.Add(email);
            }
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }
    }
}
