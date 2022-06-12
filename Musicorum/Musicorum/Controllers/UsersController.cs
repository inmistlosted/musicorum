using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicorum.Services;
using Musicorum.Services.Models;
using Musicorum.Web.Extensions;
using Musicorum.Web.Infrastructure;

namespace Musicorum.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private const int PageSize = 3;

        private readonly IUserService userService;
        private readonly ISongService songService;

        public UsersController(IUserService userService, ISongService songService)
        {
            this.userService = userService;
            this.songService = songService;
        }

        public IActionResult Index(int? page)
        {
            UserAccountModel user = this.userService.UserDetailsFriendsCommentsAndPosts(this.User.GetUserId(), page ?? 1, PageSize);

            return this.ViewOrNotFound(user);
        }

        [Authorize]
        public IActionResult AccountDetails(string id)
        {
            if (User.GetUserId() == id)
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.FullAuthorization;
            }
            else
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.NoAuthorization;
            }

            UserAccountModel user = this.userService.UserDetails(id);

            return this.ViewOrNotFound(user);
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = this.userService.All(page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
            else
            {
                var users = this.userService.UsersBySearchTerm(searchTerm, page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
        }
    }
}