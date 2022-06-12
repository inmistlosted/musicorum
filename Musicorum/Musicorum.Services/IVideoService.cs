using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface IVideoService : IService
    {
        void Create(IFormFile video, int eventId);

        bool VideoExists(int videoId);

        void Delete(int videoId);

        void DeleteAllVideosOfEvent(int eventId);
        IList<VideoModel> GetEventVideos(int eventId);
    }
}
