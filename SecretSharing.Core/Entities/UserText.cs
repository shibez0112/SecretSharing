using SecretSharing.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSharing.Core.Entities
{
    public class UserText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool IsAutoDeleted { get; set; }

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
