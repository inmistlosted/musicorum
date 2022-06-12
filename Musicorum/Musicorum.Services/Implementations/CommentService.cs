using AutoMapper.QueryableExtensions;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Musicorum.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly MusicorumDbContext db;

        public CommentService(MusicorumDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CommentModel> CommentsBySongId(int songId)
        {
            return this.db.Comments.Where(c => c.SongId == songId).OrderByDescending(c => c.Date).ProjectTo<CommentModel>().ToList();
        }

        public void Create(string commentText, string userId, int songId)
        {
            var comment = new Comment
            {
                Date = DateTime.UtcNow,
                Text = commentText,
                UserId = userId,
                SongId = songId
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public void DeleteCommentsBySongId(int songId)
        {
            var comments = this.db.Comments.Where(c => c.SongId == songId);

            foreach (var comment in comments)
            {
                this.db.Remove(comment);
            }

            this.db.SaveChanges();
        }

        public void Delete(int commentId)
        {
            Comment comment = this.db.Comments.Find(commentId);
            this.db.Remove(comment);
            this.db.SaveChanges();
        }
    }
}