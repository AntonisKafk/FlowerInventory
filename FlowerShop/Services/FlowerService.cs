using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FlowerService> _logger;

        public FlowerService(ApplicationDbContext context, ILogger<FlowerService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Flower> CreateFlowerAsync(Flower flower)
        {
            try
            {
                _context.Flowers.Add(flower);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower with ID {FlowerId} created.", flower.Id);
                return flower;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flower {FlowerName}", flower.Name);
                throw;
            }
        }

        public async Task<bool> DeleteFlowerAsync(int id)
        {
            var flower = await GetFlowerByIdAsync(id);
            if (flower == null)
            {
                _logger.LogWarning("Flower with ID {FlowerId} not found for deletion.", id);
                return false;
            }
            try
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower with ID {FlowerId} deleted.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flower with ID {FlowerId}", id);
                throw;
            }
        }

        public async Task<List<Flower>> GetAllFlowersAsync()
        {
            try
            {
                var flowers = await _context.Flowers.ToListAsync();
                _logger.LogInformation("Retrieved {FlowerCount} flowers.", flowers.Count);
                return flowers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all flowers");
                throw;
            }
        }

        public async Task<Flower?> GetFlowerByIdAsync(int id)
        {
            try
            {
                var result = await _context.Flowers.FindAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Flower with ID {FlowerId} not found.", id);
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flower with ID {FlowerId}", id);
                throw;
            }
        }

        public async Task<List<Flower>> GetFlowersByCategoryAsync(int categoryId)
        {
            try
            {
                var flowers = await _context.Flowers
                .Where(f => f.CategoryId == categoryId)
                .ToListAsync();
                _logger.LogInformation("Retrieved list of flowers for category ID {CategoryId}.", categoryId);
                return flowers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flowers for category ID {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<List<Flower>> SearchFlowersAsync(string searchTerm)
        {
            try
            {
                var flowers = await _context.Flowers.Where(f => f.Name.Contains(searchTerm)).ToListAsync();
                _logger.LogInformation("Retrieved {FlowerCount} flowers matching search term '{SearchTerm}'.", flowers.Count, searchTerm);
                return flowers;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching flowers with term {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<Flower> UpdateFlowerAsync(Flower flower)
        {
            try 
            {
                var existingFlower = await GetFlowerByIdAsync(flower.Id);
                if (existingFlower == null)
                {
                    _logger.LogWarning("Flower with ID {FlowerId} not found for update.", flower.Id);
                    throw new KeyNotFoundException($"Flower with ID {flower.Id} not found.");
                }
                existingFlower.Name = flower.Name;
                existingFlower.CategoryId = flower.CategoryId;
                existingFlower.Price = flower.Price;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Flower with ID {FlowerId} updated.", flower.Id);
                return existingFlower;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flower with ID {FlowerId}", flower.Id);
                throw;
            }
        }
    }
}
