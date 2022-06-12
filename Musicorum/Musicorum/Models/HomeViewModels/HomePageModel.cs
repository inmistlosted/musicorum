using Musicorum.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musicorum.Web.Models.HomeViewModels
{
    public class HomePageModel
    {
        public IList<NewsModel> News { get; set; }
        public IList<SongModel> Songs { get; set; }
        public IList<EventModel> Events { get; set; }
    }
}
