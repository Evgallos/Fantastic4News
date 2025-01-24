using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fantastic4News.Controllers
{
    [Authorize]
    public class NewsLetterController : Controller
    {
        // Injections

        private readonly INewsLetterService _newsLetterService;
        private readonly UserManager<User> _userManager;

        public NewsLetterController(INewsLetterService newsLetterService, UserManager<User> userManager)
        {
            _newsLetterService = newsLetterService;
            _userManager = userManager;
        }

        // Actions

        public IActionResult Index()
        {
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool hasNewsletter = _newsLetterService.CheckIfUserHasNewsLetter(usrId);

            return View(hasNewsletter);
        }

        public IActionResult SubscribeNewsletter()
        {
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _newsLetterService.NewsletterChange(usrId);

            return RedirectToAction("Index");
        }

        public IActionResult UnsubscribeNewsletter()
        {
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _newsLetterService.NewsletterChange(usrId);

            return RedirectToAction("Index");
        }
    }
}
