using Musicorum.Data;
using Musicorum.Data.Entities;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Musicorum.Services.Models;
using AutoMapper.QueryableExtensions;
using Musicorum.Services.Classes;

namespace Musicorum.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly MusicorumDbContext db;

        public NewsService(MusicorumDbContext db)
        {
            this.db = db;
        }

        public void Create(string title, string text, string shortDescription, IFormFile photo, bool isFavorite = false)
        {
            News news = new News
            {
                Title = title,
                Text = text,
                ShortDescription = shortDescription,
                Date = DateTime.UtcNow,
                Photo = photo != null ? FileHelper.FileAsBytes(photo) : null,
                IsFavorite = isFavorite
            };

            db.News.Add(news);
            db.SaveChanges();
        }

        public void Delete(int newsId)
        {
            var news = this.db.News.Find(newsId);
            this.db.Remove(news);
            this.db.SaveChanges();
        }

        public void Edit(int newsId, string title, string text, string shortDescription, IFormFile photo, bool isFavorite)
        {
            var news = this.db.News.Find(newsId);
            news.Title = title;
            news.Text = text;
            news.ShortDescription = shortDescription;
            news.IsFavorite = isFavorite;
            news.Photo = photo == null ? news.Photo : FileHelper.FileAsBytes(photo);
            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.News.Any(p => p.Id == id);

        public NewsModel NewsById(int newsId)
        {
            return this.db.News.Where(p => p.Id == newsId).ProjectTo<NewsModel>().FirstOrDefault();
        }

        public IList<NewsModel> GetAll()
        {
            return this.db.News.ProjectTo<NewsModel>().OrderBy(x => x.Date).ToList();
        }

        public IList<NewsModel> GetAllFavorites()
        {
            return this.db.News.ProjectTo<NewsModel>().Where(x => x.IsFavorite).OrderBy(x => x.Date).ToList();
        }

        public long CountAllNews()
        {
            return this.db.News.Count();
        }

        public IList<NewsModel> GetNewsWithTake(int skip, int take)
        {
            return this.db.News.ProjectTo<NewsModel>().OrderBy(x => x.Date).Skip(skip).Take(take).ToList();
        }

        public IList<NewsModel> GetNews(int sort, string query, int page, int onPage)
        {
            page -= 1;

            switch (sort)
            {
                case 1:
                    return this.db
                        .News
                        .Where(x => x.Title.Contains(query))
                        .ProjectTo<NewsModel>()
                        .OrderBy(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                case 2:
                    return this.db
                        .News
                        .Where(x => x.Title.Contains(query))
                        .ProjectTo<NewsModel>()
                        .OrderByDescending(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                default:
                    return new List<NewsModel>();
            }
        }

        public int GetNewsCount(string query)
        {
            return this.db
                       .News
                       .Where(x => x.Title.Contains(query))
                       .Count();
        }
    }
}
