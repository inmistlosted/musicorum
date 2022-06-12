using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface ICommentService : IService
    {
        void Create(string commentText, string userId, int songId);

        void DeleteCommentsBySongId(int postId);
        void Delete(int commentId);

        IEnumerable<CommentModel> CommentsBySongId(int songId);
    }
}