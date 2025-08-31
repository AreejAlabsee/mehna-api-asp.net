using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MehnahFinalMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, StringLength(10)]
        public string UserType { get; set; } // "worker" أو "client"

        public string ProfileImage { get; set; } // رابط الصورة

        public ICollection<Work> Works { get; set; } = new List<Work>();
    }
}
