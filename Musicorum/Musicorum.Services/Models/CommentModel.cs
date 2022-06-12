using AutoMapper;
using Musicorum.Common.Mapping;
using Musicorum.Data.Entities;
using System;

namespace Musicorum.Services.Models
{
    public class CommentModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public int SongId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentModel>()
                .ForMember(c => c.UserFullName, cfg => cfg.MapFrom(c => c.User.FirstName + " " + c.User.LastName));
        }
    }
}