using System;
using System.Collections.Generic;

namespace Musicorum.Services.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime DateStarts { get; set; }

        public DateTime DateEnds { get; set; }

        public IList<PhotoModel> Photos { get; set; }
        public IList<VideoModel> Videos { get; set; }
    }
}