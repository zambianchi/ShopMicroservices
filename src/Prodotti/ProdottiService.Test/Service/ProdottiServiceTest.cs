using Moq;
using Moq.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Services;
using ProdottiService.Test.MockedData;

namespace ProdottiService.Test.Service
{
    public class ProdottiServiceTest
    {
        public ProdottiServiceTest() { }

        [Fact]
        public async Task GetProducts_ListaMock_ReturnLista()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            //Act
            var getProductsResult = await productService.GetProducts(new CancellationToken());

            //Assert
            Assert.NotNull(getProductsResult);
            Assert.Equal(ProductsMock.GetMockedProducts().Count, getProductsResult.Count());
        }

        [Fact]
        public async Task GetProducts_ListaVuota_ReturnLista()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getProductsResult = await productService.GetProducts(new CancellationToken());

            // Assert
            Assert.NotNull(getProductsResult);
            Assert.Equal(ProductsMock.GetMockedProducts().Count, getProductsResult.Count());
        }

        [Fact]
        public async Task GetProduct_GetByIdOk_ReturnProduct()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getProductResult = await productService.GetProduct(firstElement.Id, new CancellationToken());

            // Assert
            Assert.NotNull(getProductResult);
            Assert.Equal(firstElement.Id, getProductResult.Id);
            Assert.Equal(firstElement.Nome, getProductResult.Nome);
            Assert.Equal(firstElement.Prezzo, getProductResult.Prezzo);
        }

        [Fact]
        public async Task GetProduct_GetByIdKo_Throw()
        {
            // Arrange
            var productIdNotExist = 0;

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            Task result() => productService.GetProduct(productIdNotExist, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}
