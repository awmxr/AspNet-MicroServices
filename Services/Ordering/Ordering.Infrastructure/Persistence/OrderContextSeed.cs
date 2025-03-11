using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public async Task SeedAsync(OrderContext orderContext , ILogger<OrderContextSeed> logger)
        {
            if (!await orderContext.Orders.AnyAsync())
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }


        public static IEnumerable<Order> GetPreconfiguredOrders()
        {

            return new List<Order>
            {
                new Order
                {
                     UserName = "Amir",
                     EmailAddress = "test@test.com",
                     City = "krj",
                     Country = "Iran",
                     BankName = "mellat",
                     FirstName = "Amir",
                     LastName = "Marvasti",
                     PaymentMethod = 1,
                     RefCode = "121212121",
                     TotalPrice = 10000,
                }
            };



        }

    }


}
