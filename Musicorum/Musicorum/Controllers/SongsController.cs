using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Services.Classes;
using Musicorum.Services.Models;
using Musicorum.Web.Extensions;
using Musicorum.Web.Models.Songs;

namespace Musicorum.Web.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly ICommentService commentService;
        private readonly IGenreService genreService;
        private readonly IUserService userService;

        public SongsController(ISongService songService, ICommentService commentService, IGenreService genreService, IUserService userService)
        {
            this.songService = songService;
            this.commentService = commentService;
            this.genreService = genreService;
            this.userService = userService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Like(int songId)
        {
            bool added = this.songService.Like(songId, User.GetUserId());

            return Json(added);
        }

        [HttpGet]
        public IActionResult GetSong(int songId)
        {
            SongModel song = this.songService.SongById(songId, User.GetUserId());
            IEnumerable<CommentModel> comments = this.commentService.CommentsBySongId(songId);

            song.Comments = comments;

            return View("_SongPage", song);
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<SongModel> songs = this.songService.GetSongs(User.GetUserId());
            IList<GenreModel> genres = this.genreService.AllGenres();

            SongsPageModel model = new SongsPageModel
            {
                Songs = songs,
                Genres = genres,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GenrePage(int genreId)
        {
            GenreModel genre = this.genreService.GenreById(genreId);

            IList<SongModel> songs = this.songService.GetSongsOfGenre(genreId, User.GetUserId());

            GenrePageModel model = new GenrePageModel
            {
                Genre = genre,
                SongsOfGenre = songs,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            IList<SongModel> songs = this.songService.GetSongsByTitle(query, User.GetUserId());
            IList<GenreModel> genres = this.genreService.AllGenres();

            SearchResultModel model = new SearchResultModel
            {
                Songs = songs,
                Genres = genres,
                Query = query,
            };

            return View("SearchResult", model);
        }

        [HttpGet]
        public IActionResult AddSong(bool isAdminPage = false)
        {
            IList<GenreModel> genres = this.genreService.AllGenres();

            SongFormModel model = new SongFormModel
            {
                Genres = genres,
                IsAdminPage = isAdminPage,
            };

            return PartialView("Create", model);
        }

        [HttpPost]
        public IActionResult AddSong(SongFormModel model)
        {
            UserModel user = userService.GetById(User.GetUserId());

            this.songService.Create(
                model.Title,
                model.Description,
                model.Chorus,
                model.Song,
                model.Photo,
                User.GetUserId(),
                model.GenreId,
                string.IsNullOrEmpty(model.Author) ? $"{user.FirstName} {user.LastName}" : model.Author);

            if (model.IsAdminPage)
            {
                return RedirectToAction("Index", "Admin", new { categoryId = 1 });
            }


            return RedirectToAction("AccountDetails", "Users", User.GetUserId());
        }

        [HttpGet]
        public IActionResult EditSong(int songId, bool isAdminPage = false)
        {
            SongModel song = songService.SongById(songId, User.GetUserId());
            IList<GenreModel> genres = this.genreService.AllGenres();

            SongFormModel model = new SongFormModel {
                SongId = song.Id,
                Title = song.Title,
                Description = song.Description,
                Chorus = song.Chorus,
                GenreId = song.Genre.Id,
                Genres = genres,
                IsEditPage = true,
                IsAdminPage = isAdminPage,
                Author = song.AuthorName,
            };

            return PartialView("Edit", model);
        }

        [HttpPost]
        public IActionResult EditSong(SongFormModel model)
        {
            this.songService.Edit(
                model.SongId,
                model.Title,
                model.Description,
                model.Chorus,
                model.GenreId,
                model.Song,
                model.Photo,
                model.Author
                );

            if (model.IsAdminPage)
            {
                return RedirectToAction("Index", "Admin", new { categoryId = 1 });
            }

            return PartialView("Edit", model);
        }

        [HttpDelete]
        public IActionResult DeleteSong(int songId)
        {
            songService.Delete(songId);

            return Json( new { message = "Успішно видалено"});
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return PartialView("_CreateGenre");
        }

        [HttpPost]
        public IActionResult AddGenre(GenreFormModel model)
        {
            this.genreService.Create(model.Title, model.Description, model.Photo);

            return RedirectToAction("Index", "Admin", new { categoryId = 2 });
        }

        [HttpGet]
        public IActionResult EditGenre(int genreId)
        {
            GenreModel genre = this.genreService.GenreById(genreId);

            GenreFormModel model = new GenreFormModel
            {
                GenreId = genre.Id,
                Title = genre.Title,
                Description = genre.Description,
            };

            return PartialView("_EditGenre", model);
        }

        [HttpPost]
        public IActionResult EditGenre(GenreFormModel model)
        {
            this.genreService.Edit(
                model.GenreId,
                model.Title,
                model.Description,
                model.Photo
                );

            return RedirectToAction("Index", "Admin", new { categoryId = 2 });
        }

        [HttpDelete]
        public IActionResult DeleteGenre(int genreId)
        {
            this.genreService.Delete(genreId);

            return Json(new { message = "Успішно видалено" });
        }
    }
}
