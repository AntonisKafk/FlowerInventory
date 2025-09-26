using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IFlowerService
    {
        Task<List<Flower>> GetAllFlowersAsync();
        Task<Flower?> GetFlowerByIdAsync(int id);
        Task<Flower> CreateFlowerAsync(Flower flower);
        Task<Flower> UpdateFlowerAsync(Flower flower);
        Task<bool> DeleteFlowerAsync(int id);
        Task<List<Flower>> SearchFlowersAsync(string searchTerm);
        Task<List<Flower>> GetFlowersByCategoryAsync(int categoryId);

    }
}
