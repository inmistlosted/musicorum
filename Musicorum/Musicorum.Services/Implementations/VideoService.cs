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
    public class VideoService : IVideoService
    {
        private readonly MusicorumDbContext db;

        public VideoService(MusicorumDbContext db)
        {
            this.db = db;
        }

        public void Create(IFormFile video, int eventId)
        {
            Video videoInstance = new Video
            {
                EventId = eventId,
                VideoAsBytes = video != null ? FileHelper.FileAsBytes(video) : null,
            };

            this.db.Videos.Add(videoInstance);
            this.db.SaveChanges();
        }

        public bool VideoExists(int videoId) => this.db.Videos.Any(p => p.Id == videoId);

        public void Delete(int videoId)
        {
            if (VideoExists(videoId))
            {
                Video video = this.db.Videos.Find(videoId);
                this.db.Remove(video);
                this.db.SaveChanges();
            }
        }

        public void DeleteAllVideosOfEvent(int eventId)
        {
            IList<int> videoIds = this.db.Videos.Where(x => x.EventId == eventId).Select(x => x.Id).ToList();

            foreach (int videoId in videoIds)
            {
                Delete(videoId);
            }
        }

        public IList<VideoModel> GetEventVideos(int eventId)
        {
            return this.db.Videos.Where(x => x.EventId == eventId).ProjectTo<VideoModel>().ToList();
        }
    }
}
