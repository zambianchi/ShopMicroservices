namespace OrdiniService.Test.MockedData
{
    public class OrdersMock
    {
        public static List<Models.Order> GetMockedOrders()
        {
            return new List<Models.Order>
            {
                new Models.Order{ Id = 1, Date = new DateTime(), CreationAccountId = 1},
                new Models.Order{ Id = 2, Date = new DateTime(), CreationAccountId = 1},
                new Models.Order{ Id = 3, Date = new DateTime(), CreationAccountId = 1},
                new Models.Order{ Id = 4, Date = new DateTime(), CreationAccountId = 1},
            };
        }
    }
}
