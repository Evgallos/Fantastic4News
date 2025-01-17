using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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



        [HttpGet]

        public IActionResult Category()
        {
            var cate = _categoryService.GetCategories().ToList();
            return View(cate);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var cate = _categoryService.GetCategoryById(id);
            return View(cate);
        }
        //[HttpPost]
        //public async Task<IActionResult> EditCategory( Category category)
        //{
        //    _categoryService.EditCategory(category);
        //    return RedirectToAction();
        //}

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

    }
}