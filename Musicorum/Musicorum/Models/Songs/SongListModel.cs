using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.Songs
{
    public class SongListModel
    {
        public bool IsUserPage { get; set; }
        public IList<SongModel> Songs { get; set; }
    }
}
