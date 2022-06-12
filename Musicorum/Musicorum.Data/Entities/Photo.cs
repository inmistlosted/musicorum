using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] PhotoAsBytes { get; set; }
    }
}