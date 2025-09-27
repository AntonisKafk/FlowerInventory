using FlowerShop.Data;
using FlowerShop.Models;
using FlowerShop.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FlowerShop.Tests
{
    public class FlowerCategoryServiceTests
    {
        [Test]
        public async Task getAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerCategoryService>();
            var service = new FlowerCategoryService(context, logger);
            context.FlowerCategories.AddRange(
                new FlowerCategory { Id = 1, Name = "Roses" },
                new FlowerCategory { Id = 2, Name = "Tulips" }
            );
            context.SaveChanges();
            // ACT
            var result = await service.GetAllCategoriesAsync();
            // ASSERT
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(c => c.Name == "Roses"), Is.True);
            Assert.That(result.Any(c => c.Name == "Tulips"), Is.True);
        }

        [Test]
        public async Task createCategoryAsync_ShouldAddCategory()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerCategoryService>();
            var service = new FlowerCategoryService(context, logger);
            var newCategory = new FlowerCategory { Id = 1, Name = "Daisies" };
            // ACT
            var createdCategory = await service.CreateCategoryAsync(newCategory);
            // ASSERT
            Assert.That(createdCategory.Id, Is.EqualTo(1));
            Assert.That(createdCategory.Name, Is.EqualTo("Daisies"));
            Assert.That(context.FlowerCategories.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task deleteCategoryAsync_ShouldRemoveCategory()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerCategoryService>();
            var service = new FlowerCategoryService(context, logger);
            var category = new FlowerCategory { Id = 1, Name = "Daisies" };
            context.FlowerCategories.Add(category);
            context.SaveChanges();
            // ACT
            var result = await service.DeleteCategoryAsync(1);
            // ASSERT
            Assert.That(result, Is.True);
            Assert.That(context.FlowerCategories.Count(), Is.EqualTo(0));
        }

        [Test]
        public void deleteCategoryAsync_NonExistentId_ShouldThrowException()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerCategoryService>();
            var service = new FlowerCategoryService(context, logger);

            //ACT AND ASSERT
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.DeleteCategoryAsync(999));
        }

        [Test]
        public async Task getCategoryByIdAsync_ShouldReturnCategory()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new ApplicationDbContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlowerCategoryService>();
            var service = new FlowerCategoryService(context, logger);
            var category = new FlowerCategory { Id = 1, Name = "Daisies" };
            context.FlowerCategories.Add(category);
            context.SaveChanges();
            // ACT
            var result = await service.GetCategoryByIdAsync(1);
            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Daisies"));
        }
    }
}
