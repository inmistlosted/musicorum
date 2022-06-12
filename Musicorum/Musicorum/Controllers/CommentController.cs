using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Web.Extensions;
using Musicorum.Web.Infrastructure;
using Musicorum.Web.Infrastructure.Filters;
using Musicorum.Web.Models.Comment;
using Musicorum.Services.Models;

namespace Musicorum.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ISongService songService;
        private readonly ICommentService commentService;

        public CommentController(ISongService songService, ICommentService commentService)
        {
            this.songService = songService;
            this.commentService = commentService;
        }

        public IActionResult Create(int songId)
        {
            SongCommentCreateModel songCommentCreateModel = new SongCommentCreateModel {
                SongId = songId,
            };

            return PartialView(songCommentCreateModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(SongCommentCreateModel model)
        {
            if (CoreValidator.CheckIfStringIsNullOrEmpty(model.CommentText))
            {
                ModelState.AddModelError(string.Empty, "Не можливо залишити пустий коментар!");
                return PartialView(model);
            }

            this.commentService.Create(model.CommentText, User.GetUserId(), model.SongId);
            this.songService.IncreaseCommentsCount(model.SongId);
            return RedirectToAction("GetSong", "Songs", new { songId = model.SongId });
        }

        [HttpDelete]
        public IActionResult DeleteComment(int commentId)
        {
            this.commentService.Delete(commentId);

            return Json(new { message = "Успішно видалено" });
        }
    }
}