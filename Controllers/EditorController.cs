using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Controllers
{
	public class EditorController : Controller
	{
		private readonly IArticleService _articleService;

		public EditorController(IArticleService articleService)
		{
			_articleService = articleService;
		}

		public IActionResult Index()
		{
			var articles = _articleService.GetArticlesWithJournalist().ToList();
			return View(articles);
		}


	}
}
