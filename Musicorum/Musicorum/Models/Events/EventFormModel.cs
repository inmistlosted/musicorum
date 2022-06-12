using Microsoft.AspNetCore.Http;
using Musicorum.Data;
using Musicorum.Services.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Web.Models.Events
{
    public class EventFormModel
    {
        public int EventId { get; set; }

        [Url(ErrorMessage = "Не правильний формат посилання")]
        [Display(Name ="Посилання на фото")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка заголовок події")]
        [StringLength(DataConstants.MaxEventTitleLength, ErrorMessage = "{0} повинна бути щонайменш {2} та не більше ніж {1} символів.", MinimumLength = 2)]
        [Display(Name = "Заголовок події")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка місце проведення події")]
        [Display(Name = "Місце проведення")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка опис події")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка дату початку події")]
        [Display(Name = "Дата початку")]
        public DateTime DateStarts { get; set; }

        [Required(ErrorMessage = "Введіть будь-ласка дату завершення події")]
        [Display(Name = "Дата завершення")]
        //[IsDateAfter("DateStarts", false, ErrorMessage = "Дата завершення події не може бути менше або дорівнювати даті початку")]
        public DateTime DateEnds { get; set; }

        [Display(Name = "Фотографії")]
        public IList<IFormFile> Photos { get; set; }

        [Display(Name = "Відео")]
        public IList<IFormFile> Videos { get; set; }
    }
}