using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Web.Models;
using Musicorum.Services.Models;
using Musicorum.Web.Models.Admin;

namespace Musicorum.Web.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private const int songsOnPage = 3;
        private const int genresOnPage = 2;
        private const int newsOnPage = 2;
        private const int eventsOnPage = 2;

        private readonly INewsService newsService;
        private readonly ISongService songService;
        private readonly IGenreService genresService;
        private readonly IEventService eventService;

        public AdminController(INewsService newsService, ISongService songService, IGenreService genresService, IEventService eventService)
        {
            this.newsService = newsService;
            this.songService = songService;
            this.genresService = genresService;
            this.eventService = eventService;
        }

        public IActionResult Index(int categoryId)
        {
            int sort = 1;
            string query = string.Empty;
            int genreId = -1;
            int page = 1;

            AdminSongsModel adminSongsModel = null;
            AdminGenresModel adminGenresModel = null;
            AdminNewsModel adminNewsModel = null;
            AdminEventsModel adminEventsModel = null;

            switch (categoryId)
            {
                case 1:
                default:
                    int songsCount = this.songService.GetSongsCount(query, genreId);
                    List<SongModel> songs = this.songService.GetSongs(sort, query, genreId, page, songsOnPage);
                    IList<GenreModel> genres = this.genresService.AllGenres();

                    adminSongsModel = new AdminSongsModel
                    {
                        Songs = songs,
                        Genres = genres,
                        Sort = sort,
                        Query = query,
                        Page = page,
                        PageCount = (int)Math.Ceiling((decimal)songsCount / (decimal)songsOnPage),
                        GenreId = -1,
                    };
                    break;
                case 2:
                    int genresCount = this.genresService.GetGenresCount(query);
                    var genress = this.genresService.GetGenres(query, page, genresOnPage);

                    adminGenresModel = new AdminGenresModel
                    {
                        Page = page,
                        PageCount = (int)Math.Ceiling((decimal)genresCount / (decimal)genresOnPage),
                        Query = query,
                        Genres = genress,
                    };

                    break;
                case 3:
                    int newsCount = this.newsService.GetNewsCount(query);
                    IList<NewsModel> news = this.newsService.GetNews(sort, query, page, newsOnPage);

                    adminNewsModel = new AdminNewsModel
                    {
                        Page = page,
                        PageCount = (int)Math.Ceiling((decimal)newsCount / (decimal)newsOnPage),
                        Query = query,
                        News = news,
                        Sort = sort,
                    };

                    break;
                case 4:
                    int eventsCount = this.eventService.GetEventsCount(query);
                    IList<EventModel> events = this.eventService.GetEvents(sort, query, page, eventsOnPage);

                    adminEventsModel = new AdminEventsModel
                    {
                        Page = page,
                        PageCount = (int)Math.Ceiling((decimal)eventsCount / (decimal)eventsOnPage),
                        Query = query,
                        Events = events,
                        Sort = sort,
                    };

                    break;
            }

            AdminModel model = new AdminModel {
                ChosenCategoryId = categoryId,
                Songs = adminSongsModel,
                Genres = adminGenresModel,
                News = adminNewsModel,
                Events = adminEventsModel,
            };

            return View(model);
        }

        public IActionResult GetSongsList(int sort, string query, int genreId, int page)
        {
            query = query == null ? string.Empty : query;

            int songsCount = this.songService.GetSongsCount(query, genreId);
            List<SongModel> songs = this.songService.GetSongs(sort, query, genreId, page, songsOnPage);
            IList<GenreModel> genres = this.genresService.AllGenres();

            AdminSongsModel model = new AdminSongsModel
            {
                Songs = songs,
                Genres = genres,
                Sort = sort,
                Query = query,
                GenreId = genreId,
                Page = page,
                PageCount = (int)Math.Ceiling((decimal)songsCount / (decimal)songsOnPage),
            };

            return PartialView("_SongsAdmin", model);
        }

        public IActionResult GetGenresList(string query, int page)
        {
            query = query == null ? string.Empty : query;

            int genresCount = this.genresService.GetGenresCount(query);
            IList<GenreModel> genres = this.genresService.GetGenres(query, page, genresOnPage);

            AdminGenresModel model = new AdminGenresModel
            {
                Page = page,
                PageCount = (int)Math.Ceiling((decimal)genresCount / (decimal)genresOnPage),
                Query = query,
                Genres = genres,
            };

            return PartialView("_GenresAdmin", model);
        }

        public IActionResult GetNewsList(int sort, string query, int page)
        {
            query = query == null ? string.Empty : query;

            int newsCount = this.newsService.GetNewsCount(query);
            IList<NewsModel> news = this.newsService.GetNews(sort, query, page, newsOnPage);

            AdminNewsModel model = new AdminNewsModel
            {
                Page = page,
                PageCount = (int)Math.Ceiling((decimal)newsCount / (decimal)newsOnPage),
                Query = query,
                News = news,
                Sort = sort,
            };

            return PartialView("_NewsAdmin", model);
        }

        public IActionResult GetEventsList(int sort, string query, int page)
        {
            query = query == null ? string.Empty : query;

            int eventsCount = this.eventService.GetEventsCount(query);
            IList<EventModel> events = this.eventService.GetEvents(sort, query, page, eventsOnPage);

            AdminEventsModel model = new AdminEventsModel
            {
                Page = page,
                PageCount = (int)Math.Ceiling((decimal)eventsCount / (decimal)eventsOnPage),
                Query = query,
                Events = events,
                Sort = sort,
            };

            return PartialView("_EventsAdmin", model);
        }
    }
}
