using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.ViewModels;
namespace Fantastic4News.ViewComponents
{
	public class LintTextNewsListViewComponent:ViewComponent
	{
        private readonly IArticleService _ias;
        private readonly ICategoryService _ics;
        public LintTextNewsListViewComponent(IArticleService ias, ICategoryService ics)
        {
            _ias = ias;
            _ics = ics;
        }
        public IViewComponentResult Invoke()
		{ 
            var articles=_ics.GetCategoriesWithAritcles();

			

			var alinktextvm = articles.Select(a => new LinkTextNewsVm
			{
				CategoryName=a.Name,
				Articles=a.Articles.OrderByDescending(ao=>ao.DateStamp).ToList()
			}).ToList();


			return View(alinktextvm);
        }
	}
}
