using FlowerShop.Interfaces;
using FlowerShop.Models;
using FlowerShop.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlowerShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFlowerCategoryService _categoryService;

        public List<FlowerCategory> Categories { get; set; } = new();

        public IndexModel(IFlowerCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategoriesAsync();
        }

        public async Task<IActionResult> OnDeleteCategory(int id)
        {
            try
            {
                //await _categoryService.DeleteCategoryAsync(id);
                return new JsonResult(new { success = true });
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }
        }
    }
}