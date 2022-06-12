using AutoMapper;
using Musicorum.Common.Mapping;
using Musicorum.Services.Models;
using Musicorum.Web.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.Comment
{
    public class SongCommentCreateModel
    {
        public int SongId { get; set; }

        [Display(Name ="Коментар:")]
        public string CommentText { get; set; }
    }
}