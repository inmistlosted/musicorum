using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface IGenreService
    {
        void Create(string title, string description, IFormFile photo = null);
        void Delete(int genreId);
        void Edit(int genreId, string title, string description, IFormFile photo = null);
        bool Exists(int id);
        GenreModel GenreById(int genreId);
        IList<GenreModel> AllGenres();
        IList<GenreModel> GetGenres(string query, int page, int onPage);
        int GetGenresCount(string query);
    }
}
