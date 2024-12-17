using Fantastic4News.Services;
using Fantastic4News.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;

namespace Fantastic4News.Controllers
{
    public class ArticleController : Controller
    {
        // Injections

        private readonly IArticleService _articleService;

        private readonly ICategoryService _categoryService;

        private readonly UserManager<User> _userManager;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
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
            obj.UserId = _userManager.GetUserId(HttpContext.User) ?? "";
            // obj.UserId = HttpContext.User.Identity.

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

        public IActionResult Edit(int id)
        {
            Article obj = _articleService.GetArticleById(id);

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
        public IActionResult Edit(ArticleIndexVM vmObj)
        {
                _articleService.UpdateArticle(vmObj.Article);

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
