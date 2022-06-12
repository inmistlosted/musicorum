using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Songs
{
    public class SearchResultModel
    {
        public string Query { get; set; }
        public IList<SongModel> Songs { get; set; }
        public IList<GenreModel> Genres { get; set; }
    }
}
