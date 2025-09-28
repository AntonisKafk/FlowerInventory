using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerShop.Pages
{
    public class FlowersModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IFlowerCategoryService _flowerCategoryService;

        public List<Flower> Flowers { get; set; } = new();
        public string PageTitle { get; set; } = string.Empty;
        public int? CurrentCategoryId { get; set; }

        public FlowersModel(IFlowerService flowerService, IFlowerCategoryService flowerCategoryService)
        {
            _flowerService = flowerService;
            _flowerCategoryService = flowerCategoryService;
        }
        public async Task OnGet(int? categoryId, string? categoryName)
        {
            CurrentCategoryId = categoryId;
            if (categoryId.HasValue)
            {
                //Get all flowers for the specified category
                Flowers = await _flowerService.GetFlowersByCategoryAsync(categoryId.Value);
                PageTitle = PageTitle = $"Available {categoryName}";
            }
            else
            {
                //Get all flowers
                PageTitle = "All Flowers Available";
                Flowers = await _flowerService.GetAllFlowersAsync();
            }
        }

        public async Task<IActionResult> OnGetDeleteFlower(int id)
        {
            try
            {
                Console.WriteLine($"Deleting flower ID: {id}");
                await _flowerService.DeleteFlowerAsync(id);
            }
            catch (Exception)
            {
                Console.WriteLine($"Deletion for flower {id} failed..");
            }
            // Preserve the category filter when redirecting
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
