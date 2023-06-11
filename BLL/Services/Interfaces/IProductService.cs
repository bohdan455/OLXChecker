using BLL.Dto;
using Database.Entities;

namespace BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task Add(ProductDto productDto);
        IEnumerable<Product> GetAll();
    }
}