using Fantastic4News.Services;
using Fantastic4News.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            obj.Views++;
            _articleService.UpdateArticle(obj);

            return View(obj);
        }

public IActionResult Create()
        {
            Article obj = new Article();
            obj.UserId = "860bc1f6-c6ed-4304-a1b5-326ae59afb20";

            SelectList categoriesSl = new SelectList(
                _categoryService.GetCategories().OrderBy(c => c.Name).ToList(),
                "Id",
                "Name"
                );

            ArticleIndexVM vmObj = new ArticleIndexVM()
            {
                Article = obj,
                CategoriesSelectList = categoriesSl
            };

            return View(vmObj);
        }

        [HttpPost]
        public IActionResult Create(ArticleIndexVM vmObj)
        {
            _articleService.CreateArticle(vmObj.Article);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult LikeArticle(int id)
        {
            Article obj = _articleService.GetArticleById(id);
            obj.Like++;
            _articleService.UpdateArticle(obj);

            return Json(obj.Like);
        }
    }
}
