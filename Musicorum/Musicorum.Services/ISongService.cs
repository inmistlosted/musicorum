using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface ISongService
    {
        void Create(string title, string description, string chorus, IFormFile songFile, IFormFile photo, string userId, int genreId, string authorName = null);
        void Delete(int songId);
        void Edit(int songId, string title, string description, string chorus, int genreId, IFormFile songFile = null, IFormFile photo = null, string authorName = null);
        bool Exists(int id);
        List<SongModel> GetSongsOfUser(string userId);
        List<SongModel> GetSongsOfUserWithTake(string userId, int skip, int take);
        List<SongModel> GetSongs(string userId);
        List<SongModel> GetSongs();
        List<SongModel> GetSongs(int sort, string query, int genreId, int page, int onPage);
        List<SongModel> GetSongsWithTake(string userId, int skip, int take);
        List<SongModel> GetSongsByTitle(string title, string userId);
        List<SongModel> GetSongsByTitleWithTake(string title, string userId, int skip, int take);
        List<SongModel> GetSongsOfGenre(int genreId, string userId);
        List<SongModel> GetSongsOfGenreWithTake(int genreId, string userId, int skip, int take);
        List<SongModel> GetUserLikedSongs(string userId);
        void DeleteSongsOfGenre(int genreId);
        bool Like(int songId, string userId);
        SongModel SongById(int songId, string userId);
        void IncreaseCommentsCount(int songId);
        int GetSongsCount(string query, int genreId);
    }
}
