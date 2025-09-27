using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerShop.Pages
{
    public class ManageCategoryModel : PageModel
    {
        private readonly IFlowerCategoryService _categoryService;

        [BindProperty]
        public FlowerCategory Category { get; set; } = new FlowerCategory { Name = string.Empty };

        public ManageCategoryModel(IFlowerCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Category = await _categoryService.GetCategoryByIdAsync(id.Value);
                if (Category == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Category!.Id == 0)
            {
                await _categoryService.CreateCategoryAsync(Category);
            }
            else
            {
                await _categoryService.UpdateCategoryAsync(Category);
            }

            return RedirectToPage("/Index");
        }
    }
}
