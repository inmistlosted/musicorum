using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Services.Classes;
using Musicorum.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Musicorum.Services.Implementations
{
    public class SongService : ISongService
    {
        private readonly MusicorumDbContext db;
        private readonly ICommentService commentService;

        public SongService(MusicorumDbContext db, ICommentService commentService)
        {
            this.db = db;
            this.commentService = commentService;
        }

        public void Create(string title, string description, string chorus, IFormFile songFile, IFormFile photo, string userId, int genreId, string authorName = null)
        {
            var song = new Song
            {
                Title = title,
                Description = description,
                Chorus = chorus,
                SongAsBytes = FileHelper.FileAsBytes(songFile),
                Photo = photo != null ? FileHelper.FileAsBytes(photo) : null,
                UserId = userId,
                GenreId = genreId,
                AuthorName = authorName,
                LikesCount = 0,
                CommentsCount = 0,
                Date = DateTime.UtcNow,
            };

            db.Songs.Add(song);
            db.SaveChanges();
        }

        public void Delete(int songId)
        {
            var song = this.db.Songs.Find(songId);
            this.commentService.DeleteCommentsBySongId(songId);
            this.db.Remove(song);
            this.db.SaveChanges();
        }

        public void Edit(int songId, string title, string description, string chorus, int genreId, IFormFile songFile = null, IFormFile photo = null, string authorName = null)
        {
            var song = this.db.Songs.Find(songId);
            song.Title = title;
            song.Description = description;
            song.Chorus = chorus;
            song.SongAsBytes = songFile == null ? song.SongAsBytes : FileHelper.FileAsBytes(songFile);
            song.Photo = photo == null ? song.Photo : FileHelper.FileAsBytes(photo);
            song.GenreId = genreId;
            song.AuthorName = authorName;
            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.Songs.Any(p => p.Id == id);

        public List<SongModel> GetSongsOfUser(string userId)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.UserId == userId)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
                
        }

        public List<SongModel> GetSongsOfUserWithTake(string userId, int skip, int take)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.UserId == userId)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .Skip(skip)
                .Take(take)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetSongs(string userId)
        {
            List<SongModel> result = this.db
                .Songs
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetSongs()
        {
            return this.db
                .Songs
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();
        }

        public List<SongModel> GetSongs(int sort, string query, int genreId, int page, int onPage)
        {
            page -= 1;

            switch (sort)
            {
                case 1:
                    return this.db
                        .Songs
                        .Where(x => x.Title.Contains(query))
                        .Include(x => x.Genre)
                        .Include(x => x.User)
                        .ProjectTo<SongModel>()
                        .Where(x => genreId == -1 ? x.Title.Length > 0 : x.Genre.Id == genreId)
                        .OrderBy(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                case 2:
                    return this.db
                        .Songs
                        .Where(x => x.Title.Contains(query))
                        .Include(x => x.Genre)
                        .Include(x => x.User)
                        .ProjectTo<SongModel>()
                        .Where(x => genreId == -1 ? x.Title.Length > 0 : x.Genre.Id == genreId)
                        .OrderByDescending(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                case 3:
                    return this.db
                        .Songs
                        .Where(x => x.Title.Contains(query))
                        .Include(x => x.Genre)
                        .Include(x => x.User)
                        .ProjectTo<SongModel>()
                        .Where(x => genreId == -1 ? x.Title.Length > 0 : x.Genre.Id == genreId)
                        .OrderByDescending(x => x.LikesCount)
                        .ThenByDescending(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                case 4:
                    return this.db
                        .Songs
                        .Where(x => x.Title.Contains(query))
                        .Include(x => x.Genre)
                        .Include(x => x.User)
                        .ProjectTo<SongModel>()
                        .Where(x => genreId == -1 ? x.Title.Length > 0 : x.Genre.Id == genreId)
                        .OrderByDescending(x => x.CommentsCount)
                        .ThenByDescending(x => x.Date)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                default:
                    return new List<SongModel>();
            }

        }

        public int GetSongsCount(string query, int genreId)
        {
            return this.db
                        .Songs
                        .Where(x => x.Title.Contains(query))
                        .Include(x => x.Genre)
                        .Include(x => x.User)
                        .ProjectTo<SongModel>()
                        .Where(x => genreId == -1 ? x.Title.Length > 0 : x.Genre.Id == genreId)
                        .Count();
        }

        public List<SongModel> GetSongsWithTake(string userId, int skip, int take)
        {
            List<SongModel> result = this.db
                .Songs
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .Skip(skip)
                .Take(take)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetSongsByTitle(string title, string userId)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.Title.Contains(title))
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderBy(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetSongsByTitleWithTake(string title, string userId, int skip, int take)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.Title.Contains(title))
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderBy(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .Skip(skip)
                .Take(take)
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public void DeleteSongsOfGenre(int genreId)
        {
            IEnumerable<Song> songs = this.db.Songs.Where(x => x.GenreId == genreId);

            foreach(Song song in songs)
            {
                this.db.Remove(song);
            }
            
            this.db.SaveChanges();
        }

        public List<SongModel> GetSongsOfGenre(int genreId, string userId)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.GenreId == genreId)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetSongsOfGenreWithTake(int genreId, string userId, int skip, int take)
        {
            List<SongModel> result = this.db
                .Songs
                .Where(x => x.GenreId == genreId)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .Skip(skip)
                .Take(take)
                .ToList();

            result.ForEach(x => x.IsLiked = this.IsLiked(x.Id, userId));

            return result;
        }

        public List<SongModel> GetUserLikedSongs(string userId)
        {
            List<int> likedSongsIds = this.db.Likes.Where(x => x.UserId == userId).Select(x => x.SongId).ToList();

            List<SongModel> result = this.db
                .Songs
                .Where(x => likedSongsIds.Contains(x.Id))
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .OrderByDescending(x => x.LikesCount)
                .ThenByDescending(x => x.Date)
                .ToList();

            result.ForEach(x => x.IsLiked = true);

            return result;
        }

        public bool Like(int songId, string userId)
        {
            bool likeWasAdded = false;

            if (this.Exists(songId))
            {
                var song = this.db.Songs.Find(songId);
                Like like = this.db.Likes.FirstOrDefault(x => x.SongId == songId && x.UserId == userId);

                if(like != null)
                {
                    this.db.Likes.Remove(like);
                    song.LikesCount--;
                }
                else
                {
                    like = new Like
                    {
                        SongId = songId,
                        UserId = userId,
                    };

                    this.db.Likes.Add(like);
                    song.LikesCount++;
                    likeWasAdded = true;
                }
                
                this.db.SaveChanges();
            }

            return likeWasAdded;
        }

        public SongModel SongById(int songId, string userId)
        {
            SongModel song = this.db
                .Songs
                .Where(p => p.Id == songId)
                .Include(x => x.Genre)
                .Include(x => x.User)
                .ProjectTo<SongModel>()
                .FirstOrDefault();

            song.IsLiked = this.IsLiked(songId, userId);

            return song;
        }

        public void IncreaseCommentsCount(int songId) {
            if (this.Exists(songId))
            {
                var song = this.db.Songs.Find(songId);
                song.CommentsCount++;
                this.db.SaveChanges();
            }
        }

        private bool IsLiked(int songId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            Like like = this.db.Likes.FirstOrDefault(x => x.SongId == songId && x.UserId == userId);

            return like != null;
        }
    }
}
