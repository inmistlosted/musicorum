using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxSongFileLength)]
        public byte[] SongAsBytes { get; set; }

        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] Photo { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Chorus { get; set; }
        public string AuthorName { get; set; }

        [Range(0, int.MaxValue)]
        public long LikesCount { get; set; }

        [Range(0, int.MaxValue)]
        public long CommentsCount { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

    }
}
