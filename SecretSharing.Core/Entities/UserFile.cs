using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSharing.Core.Entities.Identity;

namespace SecretSharing.Core.Entities
{
    public class UserFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public bool IsAutoDeleted { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
