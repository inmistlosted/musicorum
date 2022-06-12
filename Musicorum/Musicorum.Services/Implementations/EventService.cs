using AutoMapper;
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

namespace Musicorum.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly MusicorumDbContext db;
        private readonly IPhotoService photoService;
        private readonly IVideoService videoService;

        public EventService(MusicorumDbContext db, IPhotoService photoService, IVideoService videoService)
        {
            this.db = db;
            this.photoService = photoService;
            this.videoService = videoService;
        }

        public void Create(string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, IList<IFormFile> photos = null, IList<IFormFile> videos = null)
        {
            Event ev = new Event
            {
                ImageUrl = imageUrl,
                Title = title,
                Location = location,
                Description = description,
                DateEnds = dateEnds,
                DateStarts = dateStarts,
            };

            this.db.Events.Add(ev);
            this.db.SaveChanges();

            if(photos != null)
            {
                foreach (IFormFile photoFile in photos)
                {
                    this.photoService.Create(photoFile, ev.Id);
                }
            }
            
            if(videos != null)
            {
                foreach (IFormFile videoFile in photos)
                {
                    this.videoService.Create(videoFile, ev.Id);
                }
            }
        }

        public void Edit(int eventId, string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, IList<IFormFile> photos = null, IList<IFormFile> videos = null)
        {
            Event ev = this.db.Events.Find(eventId);
            ev.ImageUrl = imageUrl;
            ev.Title = title;
            ev.Location = location;
            ev.Description = description;
            ev.DateEnds = dateEnds;
            ev.DateStarts = dateStarts;

            if (photos != null)
            {
                foreach (IFormFile photoFile in photos)
                {
                    Photo photo = new Photo
                    {
                        EventId = ev.Id,
                        PhotoAsBytes = photoFile != null ? FileHelper.FileAsBytes(photoFile) : null,
                    };

                    this.db.Photos.Add(photo);
                }
            }
            
            if (videos != null)
            {
                foreach (IFormFile videoFile in photos)
                {
                    Video video = new Video
                    {
                        EventId = ev.Id,
                        VideoAsBytes = videoFile != null ? FileHelper.FileAsBytes(videoFile) : null,
                    };

                    this.db.Videos.Add(video);
                }
            }

            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.Events.Any(e => e.Id == id);

        public void Delete(int eventId)
        {
            Event ev = this.db.Events.Find(eventId);
            this.photoService.DeleteAllPhotosOfEvent(eventId);
            this.videoService.DeleteAllVideosOfEvent(eventId);
            this.db.Events.Remove(ev);
            this.db.SaveChanges();
        }

        public IEnumerable<EventModel> UpcomingThreeEvents()
        {
            return this.db.Events.Where(e => e.DateEnds < DateTime.UtcNow).OrderBy(e => e.DateStarts).Take(3).ProjectTo<EventModel>().ToList();
        }

        public IList<EventModel> GetEvents()
        {
            return this.db.Events.ProjectTo<EventModel>().ToList();
        }

        public IList<EventModel> GetEventsOfTake(int skip, int take)
        {
            return this.db.Events.ProjectTo<EventModel>().Skip(skip).Take(take).OrderByDescending(x => x.DateStarts).ToList();
        }

        public EventModel EventById(int eventId)
        {
            EventModel ev = this.db.Events.Where(x => x.Id == eventId).ProjectTo<EventModel>().FirstOrDefault();
            ev.Photos = this.photoService.GetEventPhotos(ev.Id);
            ev.Videos = this.videoService.GetEventVideos(ev.Id);
            

            return this.db.Events.Where(x => x.Id == eventId).ProjectTo<EventModel>().FirstOrDefault();
        }

        public IList<EventModel> GetEvents(int sort, string query, int page, int onPage)
        {
            page -= 1;

            switch (sort)
            {
                case 1:
                    return this.db
                        .Events
                        .Where(x => x.Title.Contains(query))
                        .ProjectTo<EventModel>()
                        .OrderBy(x => x.DateStarts)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                case 2:
                    return this.db
                        .Events
                        .Where(x => x.Title.Contains(query))
                        .ProjectTo<EventModel>()
                        .OrderByDescending(x => x.DateStarts)
                        .Skip(page * onPage)
                        .Take(onPage)
                        .ToList();
                default:
                    return new List<EventModel>();
            }
        }

        public int GetEventsCount(string query)
        {
            return this.db
                       .Events
                       .Where(x => x.Title.Contains(query))
                       .Count();
        }
    }
}