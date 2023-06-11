using BLL.Dto;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions.Mappers
{
    public static class ProductExtension
    {
        public static Product ToProduct(this ProductDto dto)
        {
            return new Product
            {
                Url = dto.Url,
                Emails = new List<Email> {
                    new Email{EmailAddress=dto.Email}, 
                },
            };
        }
    }
}
