using Musicorum.Web.Models.Admin;

namespace Musicorum.Web.Models
{
    public class AdminModel
    {
        public int ChosenCategoryId { get; set; }
        public AdminSongsModel Songs { get; set; }
        public AdminGenresModel Genres { get; set; }
        public AdminNewsModel News { get; set; }
        public AdminEventsModel Events { get; set; }
    }
}
