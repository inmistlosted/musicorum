using Microsoft.AspNetCore.Http;
using Musicorum.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.PostsViewModels
{
    public class PostFormModel
    {
        [Required]
        [Display(Name = "What do you think?")]
        public string Text { get; set; }

        [Display(Name = "How do you feel?")]
        public Feeling Feeling { get; set; }

        [Display(Name = "Upload a photo")]
        public IFormFile Photo { get; set; }
    }
}