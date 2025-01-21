using Fantastic4News.Services;
using Fantastic4News.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc.Rendering;
using ImageResizer;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Fantastic4News.Models.ViewModels;
using System.Drawing.Imaging;
using System.Drawing;
using System;
using System.IO;
using ImageMagick;


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
            var articles = _articleService.GetArticles().OrderByDescending(a => a.DateStamp).AsEnumerable();

            if (categoryId != 0)
            {
                articles = articles.Where(a => a.CategoryId == categoryId);

                ViewBag.CategoryName = _categoryService.GetCategoryById(categoryId).Name;
            }

            if (search != null)
            {
                search = search.Trim();
                articles = articles.Where(a => a.Content.ToUpper().Contains(search.ToUpper()) || a.HeadLine.ToUpper().Contains(search.ToUpper()) || a.LinkText.ToUpper().Contains(search.ToUpper()));
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

        [Authorize(Roles = "Journalist,Admin")]
        public IActionResult Create()
        {
            Article obj = new Article();
            obj.UserId = _userManager.GetUserId(HttpContext.User) ?? "";

            SelectList categoriesSl = new SelectList(
                _categoryService.GetCategories().OrderBy(c => c.Name).ToList(),
                "Id",
                "Name"
                );

            ArticleIndexVM vmObj = new ArticleIndexVM()
            {
                Article = obj,
                CategoriesSelectList = categoriesSl,
            };

            return View(vmObj);
        }

        [HttpPost]
        public IActionResult Create(ArticleIndexVM vmObj)
        {
            if (vmObj.Article.ImageFile == null || string.IsNullOrEmpty(vmObj.Article.ImageFile.FileName))
            {
                ModelState.AddModelError("", "Please upload a valid image file.");
                return View(vmObj);
            }

            string uniqueFileName = AddGuidToFile(vmObj.Article.ImageFile.FileName);
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            string uniqueFilePath = Path.Combine(uploadFolder, uniqueFileName);

            try
            {
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Save image to folder

                using (var fileStream = new FileStream(uniqueFilePath, FileMode.Create))
                {
                    vmObj.Article.ImageFile.CopyTo(fileStream);

                }

                // ImageMagick 

                /// Read from file
                using var image = new MagickImage(uniqueFilePath);

                var size = new MagickGeometry(900, 500);
                size.IgnoreAspectRatio = true;

                image.Resize(size);
                image.Write("tempImage.JPG");

                FileStream newStream = new FileStream("tempImage.JPG", FileMode.Open);

                // Logic for sending image to Azure blob storage

                _fileService.UploadFileToContainer(uniqueFileName, newStream);

                if (System.IO.File.Exists(uniqueFilePath))
                {
                    System.IO.File.Delete(uniqueFilePath);
                }

                vmObj.Article.ImageLink = $"https://fantasticfourstorage.blob.core.windows.net/articleimages/{uniqueFileName}";

                // Create article

                _articleService.CreateArticle(vmObj.Article);

                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error uploading the file: {ex.Message}");
                return View(vmObj);
            }
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
