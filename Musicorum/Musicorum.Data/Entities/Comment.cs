using System;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; }
    }
}