using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface IPhotoService : IService
    {
        void Create(IFormFile photo, int eventId);

        bool PhotoExists(int photoId);

        void Delete(int photoId);
        void DeleteAllPhotosOfEvent(int eventId);
        IList<PhotoModel> GetEventPhotos(int eventId);
    }
}