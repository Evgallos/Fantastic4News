using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.ViewComponents
{
    public class CategoryIndexViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryIndexViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var objList = _categoryService.GetCategories();

            return View(objList);
        }
    }
}
