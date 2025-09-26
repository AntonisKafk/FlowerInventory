using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IFlowerCategoryService
    {
        Task<List<FlowerCategory>> GetAllCategoriesAsync();
        Task<FlowerCategory> GetCategoryByIdAsync(int id);
        Task<FlowerCategory> CreateCategoryAsync(FlowerCategory category);
        Task<FlowerCategory> UpdateCategoryAsync(FlowerCategory category);
        Task<bool> DeleteCategoryAsync(int id); 
    }
}
 