using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Web.Models.News
{
    public class NewsPageModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public IList<NewsModel> News { get; set; }
        public bool HasMore { get; set; }
    }
}
