using BLL.Dto;
using Web.Models;

namespace Web.Extensions.Mappers
{
    public static class OlxSubcribeModelExtensions
    {
        public static ProductDto ToProductDto(this OlxSubscribeModel olxSubscribeModel)
        {
            return new ProductDto
            {
                Email = olxSubscribeModel.Email,
                Url = olxSubscribeModel.Url
            };
        }
    }
}
