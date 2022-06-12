using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Admin
{
    public class AdminSongsModel
    {
        public int Sort { get; set; }
        public int Page { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public string Query { get; set; }
        public IList<SongModel> Songs { get; set; }
        public IList<GenreModel> Genres { get; set; }
    }
}
