using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musicorum.Data.Entities
{
    public class User : IdentityUser
    {
        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [Range(DataConstants.MinUserAge, DataConstants.MaxUserAge)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}