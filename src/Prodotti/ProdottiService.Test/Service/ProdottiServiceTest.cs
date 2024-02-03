using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Moq.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Models.API.Entity;
using ProdottiService.Models.API.Request;
using ProdottiService.Models.DB;
using ProdottiService.Services;
using ProdottiService.Test.MockedData;
using System.Data.Common;
using System.Threading;

namespace ProdottiService.Test.Service
{
    public class ProdottiServiceTest
    {
        public ProdottiServiceTest() { }

        #region GetProducts
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
        #endregion

        #region GetProduct
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
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }
        #endregion

        #region GetSpecificProducts
        [Fact]
        public async Task GetSpecificProducts_GetByIdsOk_ReturnProducts()
        {
            // Arrange
            var elements = ProductsMock.GetMockedProducts().Take(2);
            var elementsId = elements.Select(x => x.Id).ToList();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsResult = await productService.GetSpecificProducts(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsResult);
            Assert.Equal(elementsId.Count, getSpecificProductsResult.Count);

            // Verifica che i prodotti restituiti abbiano gli stessi ID dei prodotti specificati
            foreach (var product in getSpecificProductsResult)
            {
                Assert.Contains(product.Id, elementsId);
            }
        }

        [Fact]
        public async Task GetSpecificProducts_GetByIdsNotExist_ReturnZeroProducts()
        {
            // Arrange
            var elements = ProductsMock.GetMockedProducts();
            var maxElementsId = elements.Max(x => x.Id);
            var elementsId = new List<long> { maxElementsId + 1, maxElementsId + 2 };

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsResult = await productService.GetSpecificProducts(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsResult);
            Assert.Empty(getSpecificProductsResult);
        }

        [Fact]
        public async Task GetSpecificProducts_GetByEmpty_ReturnZeroProducts()
        {
            // Arrange
            var elementsId = new List<long>();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsResult = await productService.GetSpecificProducts(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsResult);
            Assert.Empty(getSpecificProductsResult);
        }
        #endregion

        #region GetSpecificProductsAvailabilities
        [Fact]
        public async Task GetSpecificProductsAvailabilities_GetByIdsOk_ReturnProducts()
        {
            // Arrange
            var elements = ProductsMock.GetMockedProducts().Take(2);
            var elementsId = elements.Select(x => x.Id).ToList();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsAvailabilitiesResult = await productService.GetSpecificProductsAvailabilities(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsAvailabilitiesResult);
            Assert.Equal(elementsId.Count, getSpecificProductsAvailabilitiesResult.ProductsAvailabilities.Count);

            // Verifica che i prodotti restituiti abbiano gli stessi ID dei prodotti specificati
            foreach (var product in getSpecificProductsAvailabilitiesResult.ProductsAvailabilities)
            {
                var elementOrigin = elements.Where(x => x.Id == product.Id).First();

                Assert.Contains(product.Id, elementsId);
                Assert.Equal(product.Availability, elementOrigin.QuantitaDisponibile);
            }
        }

        [Fact]
        public async Task GetSpecificProductsAvailabilities_GetByIdsNotExist_ReturnZeroProducts()
        {
            // Arrange
            var elements = ProductsMock.GetMockedProducts();
            var maxElementsId = elements.Max(x => x.Id);
            var elementsId = new List<long> { maxElementsId + 1, maxElementsId + 2 };

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsAvailabilitiesResult = await productService.GetSpecificProductsAvailabilities(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsAvailabilitiesResult);
            Assert.Empty(getSpecificProductsAvailabilitiesResult.ProductsAvailabilities);
        }

        [Fact]
        public async Task getSpecificProductsAvailabilitiesResult_GetByEmpty_ReturnZeroProducts()
        {
            // Arrange
            var elementsId = new List<long>();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            var getSpecificProductsAvailabilitiesResult = await productService.GetSpecificProductsAvailabilities(elementsId, new CancellationToken());

            // Assert
            Assert.NotNull(getSpecificProductsAvailabilitiesResult);
            Assert.Empty(getSpecificProductsAvailabilitiesResult.ProductsAvailabilities);
        }
        #endregion

        #region CreateProduct
        [Fact]
        public async Task CreateProduct_ProductOk_Ok()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()));

            var productService = new ProductService(mockContext.Object);

            var newProduct = ProductDTO.ProductDTOFactory(50, "Stampa fotografica fine", "Carta fine", 0.2, 492);

            CreateProductRequest request = CreateProductRequest.CreateProductRequestFactory(newProduct.Nome, newProduct.Descrizione, newProduct.Prezzo, newProduct.QuantitaDisponibile);
            var cancellationToken = new CancellationToken();

            // Act
            var createProductrResult = await productService.CreateProduct(request, cancellationToken);

            // Assert
            Assert.NotNull(createProductrResult);
            mockContext.Verify(x => x.Products.AddAsync(It.IsAny<Product>(), cancellationToken), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
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
        #endregion

        #region DeleteProduct
        [Fact]
        public async Task DeleteProduct_IdExist_Ok()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);
            var cancellationToken = new CancellationToken();

            // Act
            await productService.DeleteProduct(firstElement.Id, cancellationToken);

            // Assert
            mockContext.Verify(x => x.Products.Remove(It.IsAny<Product>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_IdNotExist_Ko()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            // Act
            Task result() => productService.DeleteProduct(-1, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }

        [Fact]
        public async Task DeleteProduct_IdExist_DbUpdateKo()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            var productService = new ProductService(mockContext.Object);

            // Act
            Task result() => productService.DeleteProduct(firstElement.Id, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(result); // Errore SaveChangesAsync
            mockContext.Verify(x => x.Products.Remove(It.IsAny<Product>()), Times.Once);
        }
        #endregion

        #region EditProduct
        [Fact]
        public async Task EditProduct_IdExistAmountChanged_Ok()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);
            var cancellationToken = new CancellationToken();

            int newQuantita = firstElement.QuantitaDisponibile + 10;

            var editProductRequest = EditProductRequest.EditProductRequestFactory(firstElement.Id, firstElement.Nome, firstElement.Descrizione,
                firstElement.Prezzo, newQuantita, firstElement.CategoryId);

            // Act
            var editedProductResult = await productService.EditProduct(editProductRequest, cancellationToken);

            // Assert
            Assert.Equal(editedProductResult.QuantitaDisponibile, newQuantita);
            Assert.Equal(editedProductResult.Nome, firstElement.Nome);
            Assert.Equal(editedProductResult.Prezzo, firstElement.Prezzo);
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task EditProduct_IdNotExist_Ko()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            int newQuantita = firstElement.QuantitaDisponibile + 10;

            var editProductRequest = EditProductRequest.EditProductRequestFactory(-1, firstElement.Nome, firstElement.Descrizione,
                firstElement.Prezzo, newQuantita, firstElement.CategoryId);

            // Act
            Task result() => productService.EditProduct(editProductRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }

        [Fact]
        public async Task EditProduct_IdExist_DbUpdateKo()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            var productService = new ProductService(mockContext.Object);

            int newQuantita = firstElement.QuantitaDisponibile + 10;
            var editProductRequest = EditProductRequest.EditProductRequestFactory(firstElement.Id, firstElement.Nome, firstElement.Descrizione,
                firstElement.Prezzo, newQuantita, firstElement.CategoryId);

            // Act
            Task result() => productService.EditProduct(editProductRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(result); // Errore SaveChangesAsync
        }
        #endregion

        #region EditProductAvailableAmount
        [Fact]
        public async Task EditProductAvailableAmount_IdExistAmountChanged_Ok()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);
            var cancellationToken = new CancellationToken();

            int removedAmount = 5;

            var editProductAvailableAmountRequest = EditProductAvailableAmountRequest.EditProductAvailableAmountRequestFactory(firstElement.Id, removedAmount);

            // Act
            var EditProductAvailableAmountResult = await productService.EditProductAvailableAmount(editProductAvailableAmountRequest, cancellationToken);

            // Assert
            Assert.Equal(EditProductAvailableAmountResult.QuantitaDisponibile, firstElement.QuantitaDisponibile - removedAmount);
            Assert.Equal(EditProductAvailableAmountResult.Nome, firstElement.Nome);
            Assert.Equal(EditProductAvailableAmountResult.Prezzo, firstElement.Prezzo);
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task EditProductAvailableAmount_IdNotExist_Ko()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            int removedAmount = 5;

            var editProductAvailableAmountRequest = EditProductAvailableAmountRequest.EditProductAvailableAmountRequestFactory(-1, removedAmount);

            // Act
            Task result() => productService.EditProductAvailableAmount(editProductAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }

        [Fact]
        public async Task EditProductAvailableAmount_IdExist_DbUpdateKo()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            var productService = new ProductService(mockContext.Object);

            int removedAmount = 5;

            var editProductAvailableAmountRequest = EditProductAvailableAmountRequest.EditProductAvailableAmountRequestFactory(firstElement.Id, removedAmount);

            // Act
            Task result() => productService.EditProductAvailableAmount(editProductAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(result); // Errore SaveChangesAsync
        }

        [Fact]
        public async Task EditProductAvailableAmount_IdExistAmountNegative_Ko()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            var productService = new ProductService(mockContext.Object);

            int removedAmount = firstElement.QuantitaDisponibile + 10; // Rimuovo la sua quantità + altri elementi

            var editProductAvailableAmountRequest = EditProductAvailableAmountRequest.EditProductAvailableAmountRequestFactory(firstElement.Id, removedAmount);

            // Act
            Task result() => productService.EditProductAvailableAmount(editProductAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Errore Quantità inferiore a 0
        }
        #endregion

        #region EditProductsAvailableAmount
        [Fact]
        public async Task EditProductsAvailableAmount_IdExistAmountChanged_Ok()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            var toEditProducts = ProductsMock.GetMockedAvailableProducts();

            var editProductsAvailableAmountRequest = EditProductsAvailableAmountRequest.EditProductsAvailableAmountRequestFactory(toEditProducts);

            // Act
            await productService.EditProductsAvailableAmount(editProductsAvailableAmountRequest, cancellationToken);

            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Exactly(toEditProducts.Count));
        }

        [Fact]
        public async Task EditProductsAvailableAmount_IdNotExist_Ko()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            var toEditProducts = ProductsMock.GetMockedAvailableProducts();
            toEditProducts.Add(ProductAvailableForRequest.ProductAvailableForRequestFactory(-1, 5));

            var editProductsAvailableAmountRequest = EditProductsAvailableAmountRequest.EditProductsAvailableAmountRequestFactory(toEditProducts);

            // Act
            Task result() => productService.EditProductsAvailableAmount(editProductsAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }

        [Fact]
        public async Task EditProductsAvailableAmount_IdExist_DbUpdateKo()
        {
            // Arrange
            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException());

            var productService = new ProductService(mockContext.Object);

            var toEditProducts = ProductsMock.GetMockedAvailableProducts();

            var editProductsAvailableAmountRequest = EditProductsAvailableAmountRequest.EditProductsAvailableAmountRequestFactory(toEditProducts);

            // Act
            Task result() => productService.EditProductsAvailableAmount(editProductsAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(result); // Errore SaveChangesAsync
        }

        [Fact]
        public async Task EditProductsAvailableAmount_IdExistAmountNegative_Ko()
        {
            // Arrange
            var firstElement = ProductsMock.GetMockedProducts().First();

            var mockContext = new Mock<ProductsContext>();
            mockContext.Setup(c => c.Products).ReturnsDbSet(ProductsMock.GetMockedProducts());

            var productService = new ProductService(mockContext.Object);

            var toEditProducts = ProductsMock.GetMockedAvailableProducts();
            toEditProducts.First().AvailableAmount = int.MaxValue;

            var editProductsAvailableAmountRequest = EditProductsAvailableAmountRequest.EditProductsAvailableAmountRequestFactory(toEditProducts);

            // Act
            Task result() => productService.EditProductsAvailableAmount(editProductsAvailableAmountRequest, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Errore Quantità inferiore a 0
        }
        #endregion
    }
}
