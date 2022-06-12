using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface INewsService
    {
        void Create(string title, string text, string shortDescription, IFormFile photo, bool isFavorite = false);
        void Delete(int newsId);
        void Edit(int newsId, string title, string text, string shortDescription, IFormFile photo, bool isFavorite);
        bool Exists(int id);
        NewsModel NewsById(int newsId);
        IList<NewsModel> GetAll();
        IList<NewsModel> GetAllFavorites();
        long CountAllNews();
        IList<NewsModel> GetNewsWithTake(int skip, int take);
        IList<NewsModel> GetNews(int sort, string query, int page, int onPage);
        int GetNewsCount(string query);
    }
}
