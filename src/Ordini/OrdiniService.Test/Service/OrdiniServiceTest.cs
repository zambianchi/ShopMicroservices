using Moq;
using Moq.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.Services;
using OrdiniService.Test.MockedData;

namespace OrdiniService.Test.Service
{
    public class OrdiniServiceTest
    {
        public OrdiniServiceTest()
        {

        }

        [Fact]
        public async Task GetListaOrdini_ListaMock_ReturnLista()
        {
            // Arrange
            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());

            var orderService = new OrderService(mockContext.Object);

            //Act
            var getOrdersResult = await orderService.GetOrders(new CancellationToken());

            //Assert
            Assert.NotNull(getOrdersResult);
            Assert.Equal(OrdersMock.GetMockedOrders().Count, getOrdersResult.Count());
        }

        [Fact]
        public async Task GetListaOrdini_ListaVuota_ReturnLista()
        {
            // Arrange
            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());

            var orderService = new OrderService(mockContext.Object);

            // Act
            var getOrdersResult = await orderService.GetOrders(new CancellationToken());

            // Assert
            Assert.NotNull(getOrdersResult);
            Assert.Equal(OrdersMock.GetMockedOrders().Count, getOrdersResult.Count());
        }

        [Fact]
        public async Task GetListaOrdini_GetByIdOk_ReturnOrder()
        {
            // Arrange
            var firstElement = OrdersMock.GetMockedOrders().First();

            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());

            var orderService = new OrderService(mockContext.Object);

            // Act
            var getOrderResult = await orderService.GetOrder(firstElement.Id, new CancellationToken());

            // Assert
            Assert.NotNull(getOrderResult);
            Assert.Equal(firstElement.Id, getOrderResult.Id);
            Assert.Equal(firstElement.Date, getOrderResult.Date);
        }

        [Fact]
        public async Task GetListaOrdini_GetByIdKo_Throw()
        {
            // Arrange
            var orderIdNotExist = 0;

            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());

            var orderService = new OrderService(mockContext.Object);

            // Act
            Task result() => orderService.GetOrder(orderIdNotExist, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}
