using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fantastic4News.Controllers
{
	[Authorize(Roles = "Journalist")]

	public class JournalistController : Controller
    {
		// Injections

		private readonly IArticleService _ias;

		public JournalistController(IArticleService ias)
        {
            _ias = ias;
        }

		// Actions

		public IActionResult Index()
        {
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var articles = _ias.GetArticlesWithJournalist().Where(a=>a.UserId==userId).ToList();
			return View(articles);
		}
		[HttpPost]
		public IActionResult ForApproveArticle(int id, bool isComplete)
		{
			var article = _ias.GetArticleById(id);
			if (article != null)
			{
				article.IsComplete = isComplete;
				article.editorsComment = "";
				_ias.UpdateArticle(article);

			}

			return RedirectToAction("Index");
		}
	}
}
