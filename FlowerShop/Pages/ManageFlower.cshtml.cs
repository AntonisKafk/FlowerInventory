using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlowerShop.Pages
{
    public class ManageFlowerModel : PageModel
    {
        private readonly IFlowerService _flowerService;
        private readonly IFlowerCategoryService _categoryService;

        [BindProperty]
        public Flower Flower { get; set; } = new Flower { Name = string.Empty, Price = 0, CategoryId = 0 };

        public List<SelectListItem> FlowerCategories { get; set; } = new List<SelectListItem>();

        public ManageFlowerModel(IFlowerService flowerService, IFlowerCategoryService flowerCategoryService)
        {
            _flowerService = flowerService;
            _categoryService = flowerCategoryService;
        }

        public async Task<IActionResult> OnGet(int? id, int? categoryId)
        {
            // Store current page in TempData
            TempData["ReturnUrl"] = Request.Headers["Referer"].ToString() ?? "/Flowers";
            // Load categories for dropdown
            var allCategories = await _categoryService.GetAllCategoriesAsync();

            if (!allCategories.Any())
            {
                TempData["Error"] = "You need to create at least one category before adding flowers.";
                return RedirectToPage("/ManageCategory");
            }

            FlowerCategories = allCategories!.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            if (id.HasValue)
            {
                Flower = await _flowerService.GetFlowerByIdAsync(id.Value);
                if (Flower == null)
                {
                    return NotFound();
                }
            }
            else if (categoryId.HasValue)
            {
                Flower.CategoryId = categoryId.Value;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Flower!.Id == 0)
            {
                await _flowerService.CreateFlowerAsync(Flower);
            }
            else
            {
                await _flowerService.UpdateFlowerAsync(Flower);
            }
            // Get return URL from TempData
            var returnUrl = TempData["ReturnUrl"]?.ToString() ?? "/Flowers";
            return Redirect(returnUrl);
        }
    }
}
