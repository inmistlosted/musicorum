using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Songs
{
    public class SongsPageModel
    {
        public IList<SongModel> Songs { get; set; }
        public IList<GenreModel> Genres { get; set; }
    }
}
