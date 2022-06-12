using System;
using System.Collections.Generic;
using System.Text;

namespace Musicorum.Services.Models
{
    public class NewsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ShortDescription { get; set; }

        public DateTime Date { get; set; }

        public byte[] Photo { get; set; }

        public bool IsFavorite { get; set; }
    }
}
