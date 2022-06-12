using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] TitlePhoto { get; set; }
    }
}
