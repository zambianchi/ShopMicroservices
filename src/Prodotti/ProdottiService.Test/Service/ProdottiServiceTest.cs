using Moq;
using Moq.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Models.API;
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

        [Fact]
        public async Task CreateProduct_ProductOk_Ok()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();

            var productService = new ProductService(mockContext.Object);

            var newProduct = ProductDTO.ProductDTOFactory(50, "Stampa fotografica fine", "Carta fine", 0.2, 492);

            CreateProductRequest request = CreateProductRequest.CreateProductRequestFactory(newProduct.Nome, newProduct.Descrizione, newProduct.Prezzo, newProduct.QuantitaDisponibile);

            // Act
            var createProductrResult = await productService.CreateProduct(request, new CancellationToken());

            // Assert
            Assert.NotNull(createProductrResult);
            Assert.Equal(createProductrResult.Prezzo, newProduct.Prezzo);
            Assert.Equal(createProductrResult.Nome, newProduct.Nome);
            Assert.Equal(createProductrResult.QuantitaDisponibile, newProduct.QuantitaDisponibile);
        }

        [Fact]
        public async Task CreateProduct_ProductSameName_Throw()
        {
            // Arrange
            var dataMock = ProductsMock.GetMockedProducts();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(dataMock);

            var productService = new ProductService(mockContext.Object);

            var newProduct = dataMock.First();

            CreateProductRequest request = CreateProductRequest.CreateProductRequestFactory(newProduct.Nome, newProduct.Descrizione, newProduct.Prezzo, newProduct.QuantitaDisponibile);

            // Act
            Task result() => productService.CreateProduct(request, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}
