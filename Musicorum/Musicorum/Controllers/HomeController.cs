using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Web.Infrastructure;
using Musicorum.Web.Infrastructure.Filters;
using Musicorum.Web.Models;
using Musicorum.Web.Models.HomeViewModels;
using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Musicorum.Services.Models;
using Musicorum.Web.Extensions;

namespace Musicorum.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int songsTake = 10;
        private const int eventsTake = 5;

        private readonly IEmailSender emailSender;
        private readonly INewsService newsService;
        private readonly ISongService songService;
        private readonly IEventService eventService;

        public HomeController(IEmailSender emailSender, INewsService newsService, ISongService songService, IEventService eventService)
        {
            this.emailSender = emailSender;
            this.newsService = newsService;
            this.songService = songService;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            IList<NewsModel> news = this.newsService.GetAllFavorites();
            IList<SongModel> songs = this.songService.GetSongsWithTake(User.GetUserId(), 0, songsTake);
            IList<EventModel> events = this.eventService.GetEventsOfTake(0, eventsTake);

            HomePageModel model = new HomePageModel
            {
                News = news,
                Songs = songs,
                Events = events,
            };


            return View(model);

            /*
            if (this.User.IsInRole(GlobalConstants.AdminRole))
            {
                return RedirectToAction("Index", "Home", new { area = GlobalConstants.AdminArea });
            }
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }*/
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateRecaptcha]
        public IActionResult Contact(EmailModel model)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"From: {model.Name} {model.Email}");
            message.AppendLine();
            message.AppendLine(model.Message);

            try
            {
                this.emailSender.SendEmailAsync(GlobalConstants.MusicorumEmail, model.Subject, message.ToString());
                ViewData["SuccessMessage"] = "Your email has been successfully sent!";
                return View(model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("EmailSenderror", "Something went wrong, please try again later!");
                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}