using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.Controllers
{
	[Authorize(Roles = "Editor")]

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
			var newarticles=articles.Where(x => x.IsComplete==true && x.IsPublished==false).ToList();
			return View(newarticles);
		}

        [HttpPost]
        public IActionResult ApproveArticle( Article ar)
        {
            var article = _articleService.GetArticleById(ar.Id);
            if (article != null)
            {
                article.DateStamp = ar.DateStamp;
                article.IsPublished = true;
                article.EditorsChoice = ar.EditorsChoice;
                article.editorsComment = "";
                article.Priority = ar.Priority;
                _articleService.UpdateArticle(article);

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RejectArticle(int id, string rejectReason)
        {
            var article = _articleService.GetArticleById(id);
            if (article != null)
            {
                article.editorsComment = rejectReason;
                article.IsPublished=false;
                article.IsComplete = false;
                _articleService.UpdateArticle(article);
           }

            return RedirectToAction("Index");
        }



    }
}
