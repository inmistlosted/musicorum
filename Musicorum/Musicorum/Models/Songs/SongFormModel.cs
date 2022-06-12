using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.Songs
{
    public class SongFormModel
    {
        public int SongId { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка назву пісні")]
        [StringLength(100, ErrorMessage = "{0} повинна бути щонайменш {2} та не більше ніж {1} символів.", MinimumLength = 2)]
        [Display(Name = "Назва пісні")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Завантажте будь-ласка пісню")]
        [Display(Name = "Завантажте пісню")]
        public IFormFile Song { get; set; }

        [Display(Name = "Обкладинка до пісні")]
        public IFormFile Photo { get; set; }

        [Display(Name = "Опис")]
        [MinLength(2), MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Куплети")]
        [MinLength(2), MaxLength(10000)]
        public string Chorus { get; set; }

        [Required]
        [Display(Name = "Жанр")]
        public int GenreId { get; set; }

        [Display(Name = "Автор")]
        [MinLength(2), MaxLength(100)]
        public string Author { get; set; }

        public bool IsEditPage { get; set; }
        public bool IsAdminPage { get; set; }

        public IList<GenreModel> Genres {get; set;}
    }
}
