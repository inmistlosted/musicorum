using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.News
{
    public class NewsFormModel
    {
        public int NewsId { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка заголовок новини")]
        [StringLength(100, ErrorMessage = "{0} повинна бути щонайменш {2} та не більше ніж {1} символів.", MinimumLength = 2)]
        [Display(Name = "Заголовок новини")]
        public string Title { get; set; }

        [Display(Name = "Головне фото новини")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка короткий опис новини")]
        [Display(Name = "Короткий опис")]
        [MinLength(2), MaxLength(1000)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка текст новини")]
        [Display(Name = "Текст новини")]
        [MinLength(2), MaxLength(10000)]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Виводити на головній?")]
        public bool IsFavorite { get; set; }
    }
}
