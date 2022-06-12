using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Services.Models;
using Musicorum.Web.Models.News;

namespace Musicorum.Web.Controllers
{
    public class NewsController : Controller
    {
        private const int take = 1;

        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            IList<NewsModel> news = this.newsService.GetNewsWithTake(0, take);
            long newsShown = this.newsService.GetNewsWithTake(0, take).Count;
            long newsCount = this.newsService.CountAllNews();

            NewsPageModel model = new NewsPageModel
            {
                Skip = take,
                HasMore = newsCount > newsShown,
                News = news
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetNews(int newsId)
        {
            NewsModel news = this.newsService.NewsById(newsId);

            return View("_ShowNews", news);
        }

        [HttpGet]
        public IActionResult GetMoreNews(int skip)
        {
            IList<NewsModel> news = this.newsService.GetNewsWithTake(skip, take);
            long newsShown = this.newsService.GetNewsWithTake(0, skip + take).Count;
            long newsCount = this.newsService.CountAllNews();

            NewsPageModel model = new NewsPageModel
            {
                Skip = skip + take,
                HasMore = newsCount > newsShown,
                News = news
            };

            return PartialView("_NewsList", model);
        }

        [HttpGet]
        public IActionResult AddNews()
        {
            return PartialView("_CreateNews");
        }

        [HttpPost]
        public IActionResult AddNews(NewsFormModel model)
        {
            this.newsService.Create(model.Title, model.Text, model.ShortDescription, model.Photo, model.IsFavorite);

            return RedirectToAction("Index", "Admin", new { categoryId = 3 });
        }

        [HttpGet]
        public IActionResult EditNews(int newsId)
        {
            NewsModel news = this.newsService.NewsById(newsId);

            NewsFormModel model = new NewsFormModel
            {
                NewsId = news.Id,
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Text = news.Text,
                IsFavorite = news.IsFavorite,
            };

            return PartialView("_EditNews", model);
        }

        [HttpPost]
        public IActionResult EditNews(NewsFormModel model)
        {
            this.newsService.Edit(
                model.NewsId,
                model.Title,
                model.Text,
                model.ShortDescription,
                model.Photo,
                model.IsFavorite
                );

            return RedirectToAction("Index", "Admin", new { categoryId = 3 });
        }

        [HttpDelete]
        public IActionResult DeleteNews(int newsId)
        {
            this.newsService.Delete(newsId);

            return Json(new { message = "Успішно видалено" });
        }
    }
}
