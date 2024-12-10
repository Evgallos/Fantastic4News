using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Controllers
{
    public class CategoryController : Controller
    {
        // Injections

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Actions

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();

            return View(categories);
        }
    }
}
