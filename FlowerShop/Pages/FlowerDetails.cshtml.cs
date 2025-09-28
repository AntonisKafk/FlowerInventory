using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerShop.Pages
{
    public class FlowerDetailsModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IFlowerCategoryService _categoryService;

        public Flower Flower { get; set; } = new();
        public string CategoryName { get; set; } = string.Empty;

        public FlowerDetailsModel(IFlowerService flowerService, IFlowerCategoryService categoryService)
        {
            _flowerService = flowerService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            // Store current page in TempData
            TempData["ReturnUrl"] = Request.Headers["Referer"].ToString() ?? "/Flowers";

            Flower = await _flowerService.GetFlowerByIdAsync(id);
            if (Flower == null)
            {
                return NotFound();
            }
            var category = await _categoryService.GetCategoryByIdAsync(Flower.CategoryId);
            CategoryName = category?.Name ?? "Unknown Category";
            return Page();
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
            // Get return URL from TempData
            var returnUrl = TempData["ReturnUrl"]?.ToString() ?? "/Flowers";
            return Redirect(returnUrl);
        }
    }
}
