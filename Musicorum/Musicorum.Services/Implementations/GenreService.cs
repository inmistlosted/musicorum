using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Services.Classes;
using Musicorum.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Musicorum.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly MusicorumDbContext db;
        private readonly ISongService songService;

        public GenreService(MusicorumDbContext db, ISongService songService)
        {
            this.db = db;
            this.songService = songService;
        }

        public void Create(string title, string description, IFormFile photo = null)
        {
            var genre = new Genre
            {
                Title = title,
                Description = description,
                TitlePhoto = photo != null ? FileHelper.FileAsBytes(photo) : null,
            };

            db.Genres.Add(genre);
            db.SaveChanges();
        }

        public void Delete(int genreId)
        {
            var genre = this.db.Genres.Find(genreId);
            this.songService.DeleteSongsOfGenre(genreId);
            this.db.Remove(genre);
            this.db.SaveChanges();
        }

        public void Edit(int genreId, string title, string description, IFormFile photo = null)
        {
            var genre = this.db.Genres.Find(genreId);
            genre.Title = title;
            genre.Description = description;
            genre.TitlePhoto = photo == null ? genre.TitlePhoto : FileHelper.FileAsBytes(photo); ;
            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.Genres.Any(p => p.Id == id);

        public GenreModel GenreById(int genreId)
        {
            return this.db.Genres.Where(p => p.Id == genreId).ProjectTo<GenreModel>().FirstOrDefault();
        }

        public IList<GenreModel> AllGenres()
        {
            return this.db.Genres.ProjectTo<GenreModel>().ToList();
        }

        public IList<GenreModel> GetGenres(string query, int page, int onPage)
        {
            return this.db.Genres.Where(x => x.Title.Contains(query)).ProjectTo<GenreModel>().Skip((page - 1) * onPage).Take(onPage).ToList();
        }

        public int GetGenresCount(string query)
        {
            return this.db.Genres.Where(x => x.Title.Contains(query)).Count();
        }
    }
}
