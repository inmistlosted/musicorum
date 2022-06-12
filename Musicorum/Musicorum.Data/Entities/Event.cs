using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxEventTitleLength)]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateStarts { get; set; }

        [Required]
        public DateTime DateEnds { get; set; }

        public IList<Photo> Photos { get; set; }
        public IList<Video> Videos { get; set; }
    }
}