using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Services.Classes;
using Musicorum.Services.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Musicorum.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly MusicorumDbContext db;

        public PhotoService(MusicorumDbContext db)
        {
            this.db = db;
        }

        public void Create(IFormFile photo, int eventId)
        {
            Photo photoInstance = new Photo
            {
                EventId = eventId,
                PhotoAsBytes = photo != null ? FileHelper.FileAsBytes(photo) : null,
            };

            this.db.Photos.Add(photoInstance);
            this.db.SaveChanges();
        }

        public bool PhotoExists(int photoId) => this.db.Photos.Any(p => p.Id == photoId);

        public void Delete(int photoId)
        {
            if (PhotoExists(photoId))
            {
                Photo photo = this.db.Photos.Find(photoId);
                this.db.Remove(photo);
                this.db.SaveChanges();
            }
        }

        public void DeleteAllPhotosOfEvent(int eventId)
        {
            IList<int> photoIds = this.db.Photos.Where(x => x.EventId == eventId).Select(x => x.Id).ToList();

            foreach(int photoId in photoIds)
            {
                Delete(photoId);
            }
        }

        public IList<PhotoModel> GetEventPhotos(int eventId)
        {
            return this.db.Photos.Where(x => x.EventId == eventId).ProjectTo<PhotoModel>().ToList();
        }
    }
}