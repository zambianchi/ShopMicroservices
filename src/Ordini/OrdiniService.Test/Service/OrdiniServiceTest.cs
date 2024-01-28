﻿using Moq;
using Moq.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.Models;
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
            await Assert.ThrowsAsync<InvalidOperationException>(result); // Sequence contains no elements
        }

        [Fact]
        public async Task CreateOrder_OrderOk_Ok()
        {
            // Arrange
            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());
            mockContext.Setup(c => c.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()));

            var orderService = new OrderService(mockContext.Object);

            var newOrder = OrderDTO.OrderDTOFactory(100, new DateTime(), 1, new List<long>());
            var productIdsList = new List<long>() { 1, 2, 3 };

            CreateOrderRequest request = CreateOrderRequest.CreateOrderRequestFactory(newOrder.CreationAccountId, newOrder.Date, productIdsList);

            var cancellationToken = new CancellationToken();

            // Act
            var createOrderResult = await orderService.CreateOrder(request, cancellationToken);

            // Assert
            Assert.NotNull(createOrderResult);
            mockContext.Verify(x => x.Orders.AddAsync(It.IsAny<Order>(), cancellationToken), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(createOrderResult.CreationAccountId, newOrder.CreationAccountId);
            Assert.Equal(createOrderResult.Date, newOrder.Date);
        }

        [Fact]
        public async Task CreateOrder_OrderWithoutProducts_Throw()
        {
            // Arrange
            var mockContext = new Mock<OrderContext>();
            mockContext.Setup(c => c.Orders).ReturnsDbSet(OrdersMock.GetMockedOrders());
            mockContext.Setup(c => c.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()));

            var orderService = new OrderService(mockContext.Object);

            var newOrder = OrderDTO.OrderDTOFactory(100, new DateTime(), 1, new List<long>());
            var productIdsList = new List<long>();

            CreateOrderRequest request = CreateOrderRequest.CreateOrderRequestFactory(newOrder.CreationAccountId, newOrder.Date, productIdsList);

            // Act
            Task result() => orderService.CreateOrder(request, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<Exception>(result); // Nessun articolo da associare al nuovo ordine
        }
    }
}
