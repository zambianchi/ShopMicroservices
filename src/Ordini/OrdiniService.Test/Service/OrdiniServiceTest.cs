using Moq;
using Moq.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.Models.API;
using OrdiniService.Services;
using OrdiniService.Test.MockedData;

namespace OrdiniService.Test.Service
{
    public class OrdiniServiceTest
    {
        public OrdiniServiceTest() { }

        [Fact]
        public async Task GetOrders_ListaMock_ReturnLista()
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
        public async Task GetOrders_ListaVuota_ReturnLista()
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
        public async Task GetOrder_GetByIdOk_ReturnOrder()
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
        public async Task GetOrder_GetByIdKo_Throw()
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

        [Fact]
        public async Task CreateOrder_OrderOk_Ok()
        {
            // Arrange
            var mockContext = new Mock<OrderContext>();

            var orderService = new OrderService(mockContext.Object);

            var newOrder = OrderDTO.OrderDTOFactory(100, new DateTime(), 1);

            CreateOrderRequest request = CreateOrderRequest.CreateOrderRequestFactory(newOrder.CreationAccountId, newOrder.Date);

            // Act
            var createOrderResult = await orderService.CreateOrder(request, new CancellationToken());

            // Assert
            Assert.NotNull(createOrderResult);
            Assert.Equal(createOrderResult.CreationAccountId, newOrder.CreationAccountId);
            Assert.Equal(createOrderResult.Date, newOrder.Date);
        }
    }
}
