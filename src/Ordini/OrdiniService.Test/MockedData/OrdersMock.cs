using OrdiniService.Models;

namespace OrdiniService.Test.MockedData
{
    public class OrdersMock
    {
        public static List<Order> GetMockedOrders()
        {
            return new List<Order>
            {
                new Order{ Id = 1, Date = new DateTime(), CreationAccountId = 1},
                new Order{ Id = 2, Date = new DateTime(), CreationAccountId = 1},
                new Order{ Id = 3, Date = new DateTime(), CreationAccountId = 1},
                new Order{ Id = 4, Date = new DateTime(), CreationAccountId = 1},
            };
        }
    }
}
