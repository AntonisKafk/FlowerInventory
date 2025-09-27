using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Services
{
    public class FlowerCategoryService : IFlowerCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FlowerCategoryService> _logger;

        public FlowerCategoryService(ApplicationDbContext context, ILogger<FlowerCategoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<FlowerCategory> CreateCategoryAsync(FlowerCategory category)
        {
            try
            {
                _context.FlowerCategories.Add(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower category with ID {CategoryId} created.", category.Id);
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flower category");
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Flower category with ID {CategoryId} not found for deletion.", id);
                    return false;
                }
                _context.FlowerCategories.Remove(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower category with ID {CategoryId} deleted.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flower category with ID {CategoryId}", id);
                throw;
            }
        }

        public async Task<List<FlowerCategory>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.FlowerCategories.ToListAsync();
                _logger.LogInformation("Retrieved {Count} flower categories.", categories.Count);
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all flower categories");
                throw;
            }
        }

        public async Task<FlowerCategory> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.FlowerCategories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Flower category with ID {CategoryId} not found.", id);
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
                _logger.LogInformation("Retrieved flower category with ID {CategoryId}.", id);
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flower category with ID {CategoryId}", id);
                throw;
            }
        }

        public async Task<FlowerCategory> UpdateCategoryAsync(FlowerCategory category)
        {
            try
            {
                var existingCategory = await _context.FlowerCategories.FindAsync(category.Id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Flower category with ID {CategoryId} not found for update.", category.Id);
                    throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
                }
                existingCategory.Name = category.Name;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower category with ID {CategoryId} updated.", category.Id);
                return existingCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flower category with ID {CategoryId}", category.Id);
                throw;
            }
        }
    }
}
