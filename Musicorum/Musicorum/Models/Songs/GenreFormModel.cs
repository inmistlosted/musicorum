
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.Songs
{
    public class GenreFormModel
    {
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка назву жанру")]
        [StringLength(100, ErrorMessage = "{0} повинна бути щонайменш {2} та не більше ніж {1} символів.", MinimumLength = 2)]
        [Display(Name = "Назва жанру")]
        public string Title { get; set; }

        [Display(Name = "Обкладинка до жанру")]
        public IFormFile Photo { get; set; }

        [Display(Name = "Опис")]
        [MinLength(2), MaxLength(1000)]
        public string Description { get; set; }
    }
}
