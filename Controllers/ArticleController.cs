using Fantastic4News.Services;
using Fantastic4News.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authorization;

using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Fantastic4News.Models.ViewModels;


namespace Fantastic4News.Controllers
{
    public class ArticleController : Controller
    {
        // Injections

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;

		public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IFileService fileService)
		{
			_articleService = articleService;
			_categoryService = categoryService;
			_userManager = userManager;
			_fileService = fileService;
		}

		// Actions


        public IActionResult Index(int categoryId, string search)
        {
            var articles = _articleService.GetArticles();

            if (categoryId != 0)
            {
                articles = articles.Where(a => a.CategoryId == categoryId);

                ViewBag.CategoryName = _categoryService.GetCategoryById(categoryId).Name;
            }

            if (search != null)
            {
                search = search.Trim();
                articles = articles.Where(a => a.Content.ToUpper().Contains(search.ToUpper()));
            }

            var articlesVM = new ArticleIndexVM()
            {
                Articles = articles
            };

            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(usrId))
            {
                ViewBag.UserIdLoggedIn = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return View(articlesVM);
        }

        [Authorize]
        public IActionResult Details(int id)
        {

            var obj = _articleService.GetArticleById(id);

            obj.Views = obj.Views + 1;
            _articleService.UpdateArticle(obj);

            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(usrId))
            {
                ViewBag.UserIdLoggedIn = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return View(obj);
        }
        //this is for upload images
		[HttpPost]

		public IActionResult UploadImage(IFormFile imageFile)

		{

			if (imageFile == null || imageFile.Length == 0)

			{

				return Content("File not selected");

			}

			_fileService.UploadFileToContainer(imageFile);
            string imgurl = "https://fantasticfourstorage.blob.core.windows.net/articleimages/" + imageFile.FileName;
			return Json(imgurl);

		}

		[Authorize(Roles = "Journalist,Admin")]
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
            //string uniqueFileName = null;
            //string imagePath = null;
            //string pathAndFile = null;

            //if (!string.IsNullOrEmpty(vmObj.Article.ImageFile.FileName))
            //{

            //    uniqueFileName = AddGuidToFile(vmObj.Article.ImageFile.FileName);
            //}

            //// Logic for finding path on disc and save to variable imagePath

            //if (uniqueFileName != null && imagePath != null)
            //{
            //    pathAndFile = imagePath + "." + uniqueFileName;
            //}

            //// Logic for sending image to Azure blob storage

            //// Adding address to blob storage into vmObj.article.ImageLink

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

        [Authorize(Roles = "Admin, Journalist")]
        public IActionResult Delete(int id)
        {
            Article obj = _articleService.GetArticleById(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Article obj)
        {
            _articleService.DeleteArticle(obj.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LikeArticle(int id)
        {
            Article obj = _articleService.GetArticleById(id);
            obj.Like++;
            _articleService.UpdateArticle(obj);

            return Json(obj.Like);
        }

        // Private actions

        private string AddGuidToFile(string fileName)
        {
            string extention = Path.GetExtension(fileName);
            string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(fileName);
            string uniqueFileName = fileNameWithoutExtention + "_" + Guid.NewGuid().ToString() + extention;
            uniqueFileName = uniqueFileName.Replace(" ", "_");

            return uniqueFileName;
        }
    }
}
