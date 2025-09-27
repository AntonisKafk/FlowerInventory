using FlowerShop.Models;
using FlowerShop.Services;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using FlowerShop.Data;

namespace FlowerShop.Tests
{
    public class FlowerServiceTests
    {
        [Test]
        public async Task getAllFlowersAsync_ShouldReturnAllFlowers()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            context.Flowers.AddRange(
                new Flower { Id = 1, Name = "Rose", CategoryId = 1, Price = 2.99m },
                new Flower { Id = 2, Name = "Tulip", CategoryId = 1, Price = 1.99m }
            );
            context.SaveChanges();
            // ACT
            var result = await service.GetAllFlowersAsync();
            // ASSERT
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(f => f.Name == "Rose"), Is.True);
            Assert.That(result.Any(f => f.Name == "Tulip"), Is.True);
        }

        [Test]
        public async Task createFlowerAsync_ShouldAddFlower()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            var newFlower = new Flower { Id = 1, Name = "Daisy", CategoryId = 1, Price = 0.99m };
            // ACT
            var createdFlower = await service.CreateFlowerAsync(newFlower);
            // ASSERT
            Assert.That(createdFlower.Id, Is.EqualTo(1));
            Assert.That(createdFlower.Name, Is.EqualTo("Daisy"));
            Assert.That(context.Flowers.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task deleteFlowerAsync_ShouldRemoveFlower()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            context.Flowers.Add(new Flower{ Id = 1, Name = "Lily", CategoryId = 1, Price = 1.49m });
            context.SaveChanges();
            // ACT
            var result = await service.DeleteFlowerAsync(1);
            // ASSERT
            Assert.That(result, Is.True);
            Assert.That(context.Flowers.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task getFlowerByIdAsync_ShouldReturnCorrectFlower()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            var newFlower = new Flower { Id = 1, Name = "Orchid", CategoryId = 1, Price = 3.99m };
            var newFlower2 = new Flower { Id = 2, Name = "Orchid2", CategoryId = 1, Price = 4.99m };
            context.Flowers.Add(newFlower);
            context.Flowers.Add(newFlower2);
            context.SaveChanges();
            // ACT
            var result = await service.GetFlowerByIdAsync(1);
            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Name, Is.EqualTo("Orchid"));
        }

        [Test]
        public async Task getFlowerByIdAsync_ShouldReturnNullForNonExistentFlower()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            // ACT
            var result = await service.GetFlowerByIdAsync(999);
            // ASSERT
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task updateFlowerAsync_ShouldModifyFlower()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerService>();
            var service = new FlowerService(context, logger);
            var flower = new Flower{ Id = 1, Name = "Sunflower", CategoryId = 1, Price = 1.29m };
            context.Flowers.Add(flower);
            context.SaveChanges();
            // ACT
            flower.Price = 1.49m;
            var updatedFlower = await service.UpdateFlowerAsync(flower);
            // ASSERT
            Assert.That(updatedFlower.Price, Is.EqualTo(1.49m));
            var flowerInDb = context.Flowers.Find(1);
            Assert.That(flowerInDb?.Price, Is.EqualTo(1.49m));
        }
    }
}
