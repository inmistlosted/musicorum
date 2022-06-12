using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Songs
{
    public class GenrePageModel
    {
        public GenreModel Genre { get; set; }
        public IList<SongModel> SongsOfGenre { get; set; }
    }
}
