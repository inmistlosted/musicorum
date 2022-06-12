using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] VideoAsBytes { get; set; }
    }
}
