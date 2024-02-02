using OrdiniService.Models.DB;

namespace OrdiniService.Test.MockedData
{
    public class OrdersMock
    {
        public static List<Order> GetMockedOrders()
        {
            return new List<Order>
            {
                new Order{ Id = 1, Date = new DateTime(), CreationAccountId = 1, DeliveryAddressId = 1, OrderProducts = OrdersMock.GetMockedOrderProducts() },
                new Order{ Id = 2, Date = new DateTime(), CreationAccountId = 1, DeliveryAddressId = 2, OrderProducts = OrdersMock.GetMockedOrderProducts() },
                new Order{ Id = 3, Date = new DateTime(), CreationAccountId = 1, DeliveryAddressId = 1, OrderProducts = OrdersMock.GetMockedOrderProducts() },
                new Order{ Id = 4, Date = new DateTime(), CreationAccountId = 1, DeliveryAddressId = 1, OrderProducts = OrdersMock.GetMockedOrderProducts() },
            };
        }

        public static List<OrderProducts> GetMockedOrderProducts()
        {
            return new List<OrderProducts>
            {
                new OrderProducts{ IdProduct = 1},
                new OrderProducts{ IdProduct = 3},
                new OrderProducts{ IdProduct = 5},
                new OrderProducts{ IdProduct = 8},
            };
        }
    }
}
