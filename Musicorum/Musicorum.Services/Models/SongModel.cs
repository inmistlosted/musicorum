using Musicorum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicorum.Services.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public byte[] SongAsBytes { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Chorus { get; set; }
        public string AuthorName { get; set; }
        public int Length { get; set; }
        public long LikesCount { get; set; }
        public long CommentsCount { get; set; }
        public bool IsLiked { get; set; }
        public GenreModel Genre { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
