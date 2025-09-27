using FlowerShop.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FlowerShop.Tests
{
    public class ApplicationDbContextTests
    {
        [Test]
        public async Task CanConnectToDatabase()
        {
            // ARRANGE
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=FlowerInventory;Trusted_Connection=true;";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // ACT & ASSERT
            using var context = new ApplicationDbContext(options);

            try
            {
                var canConnect = await context.Database.CanConnectAsync();
                Assert.That(canConnect, Is.True, "Should be able to connect to the database.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Connection failed: {ex.Message}");
            }
        }
    }
}
