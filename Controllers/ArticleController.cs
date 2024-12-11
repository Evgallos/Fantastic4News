using Fantastic4News.Services;
using Fantastic4News.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.Db;

namespace Fantastic4News.Controllers
{
    public class ArticleController : Controller
    {
        // Injections

        private readonly IArticleService _articleService;

        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        // Actions

        public IActionResult Index(int categoryId)
        {
            var articles = _articleService.GetArticles();

            if (categoryId != 0)
            {
                articles = articles.Where(a => a.CategoryId == categoryId);

                ViewBag.CategoryName = _categoryService.GetCategoryById(categoryId).Name;
            }

            var articlesVM = new ArticleIndexVM()
            {
                Articles = articles
            };

            return View(articlesVM);
        }

        public IActionResult Details(int id)
        {
            var obj = _articleService.GetArticleById(id);

            return View(obj);
        }
    }
}
