using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Admin
{
    public class AdminGenresModel
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public string Query { get; set; }
        public IList<GenreModel> Genres { get; set; }
    }
}
