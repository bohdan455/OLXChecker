using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Web.Models;

namespace Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IProductService> _productService;
        private readonly HomeController _homeController;
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        public HomeControllerTests()
        {
            _productService = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<HomeController>>();
            _homeController = new HomeController(_productService.Object, _loggerMock.Object);
        }
        [Fact]
        public void Index_ReturnView()
        {
            // Arrange

            // Act
            var result = _homeController.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName);

        }
        [Fact]
        public async Task SubscribeAsync_GivenNonValidModel_ReturnIndexView()
        {
            // Arrange
            _homeController.ModelState.AddModelError("test", "test");
            // Act
            var result = await _homeController.SubscribeAsync(Mock.Of<OlxSubscribeModel>()) as ViewResult;
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index",result.ViewName,ignoreCase:true);
        }
        [Fact]
        public async Task SubscribeAsync_GivenValidModel_ReturnIndexView()
        {
            // Arrange


            // Act
            var result = await _homeController.SubscribeAsync(Mock.Of<OlxSubscribeModel>()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName, ignoreCase: true);
        }
        
    }
}
