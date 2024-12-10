using Fantastic4News.Services;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Controllers
{
    public class ArticleController : Controller
    {
        // Injections

        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // Actions

        public IActionResult Index()
        {
            var articles = _articleService.GetArticles();

            var articlesVM = new ArticleIndexVM()
            {
                Articles = articles
            };

            return View(articlesVM);
        }
    }
}
