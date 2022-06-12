using System;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public DateTime Date { get; set; }

        public byte[] Photo { get; set; }

        public bool IsFavorite { get; set; }
    }
}
