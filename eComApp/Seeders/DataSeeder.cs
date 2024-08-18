using Bogus;
using eComApp.DB;
using eComApp.DB.Entities;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

namespace eComApp.Seeders
{
    public class DataSeeder(AppDbContext dbContext)
    {
        public async Task SeedAsync()
        {
            if (dbContext.Customers.Any() || dbContext.Orders.Any() || dbContext.OrderItems.Any() || dbContext.Products.Any())
                return;

            var customers = GenerateCustomers(10);
            await dbContext.Customers.AddRangeAsync(customers);
            await dbContext.SaveChangesAsync();

            var products = GenerateProducts(10);
            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();

            var orders = GenerateOrders(customers, products, 10);
            await dbContext.Orders.AddRangeAsync(orders);
            await dbContext.SaveChangesAsync();

            var orderItems = GenerateOrderItems(orders, products);
            await dbContext.OrderItems.AddRangeAsync(orderItems);
            await dbContext.SaveChangesAsync();
        }

        private static List<Customer> GenerateCustomers(int count)
        {
            var faker = new Faker<Customer>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.RegistrationDate, f => f.Date.Past(1));

            return faker.Generate(count);
        }

        private static List<Product> GenerateProducts(int count)
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
                .RuleFor(p => p.Price, f => f.Finance.Amount()) // Generate a decimal price
                .RuleFor(p => p.Stock, f => f.Random.Int(0, 100));

            return faker.Generate(count);
        }

        private static List<Order> GenerateOrders(List<Customer> customers, List<Product> products, int count)
        {
            var faker = new Faker<Order>()
                .RuleFor(o => o.Customer, f => f.PickRandom(customers))
                .RuleFor(o => o.OrderDate, f => f.Date.Recent())
                .RuleFor(o => o.TotalAmount, f => f.Finance.Amount())
                .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());

            return faker.Generate(count);
        }

        private static List<OrderItem> GenerateOrderItems(List<Order> orders, List<Product> products)
        {
            var faker = new Faker<OrderItem>()
                .RuleFor(oi => oi.Order, f => f.PickRandom(orders))
                .RuleFor(oi => oi.Product, f => f.PickRandom(products))
                .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 5))
                .RuleFor(oi => oi.UnitPrice, f => f.Finance.Amount());

            return faker.Generate(orders.Count * 2); // Generate approximately two items per order
        }
    }
}
