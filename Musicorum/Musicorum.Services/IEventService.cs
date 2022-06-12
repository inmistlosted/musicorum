using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface IEventService : IService
    {
        void Create(string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, IList<IFormFile> photos = null, IList<IFormFile> videos = null);
        void Edit(int eventId, string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, IList<IFormFile> photos = null, IList<IFormFile> videos = null);
        bool Exists(int id);
        void Delete(int eventId);

        IEnumerable<EventModel> UpcomingThreeEvents();

        IList<EventModel> GetEvents();
        IList<EventModel> GetEventsOfTake(int skip, int take);

        EventModel EventById(int eventId);
        IList<EventModel> GetEvents(int sort, string query, int page, int onPage);
        int GetEventsCount(string query);
    }
}