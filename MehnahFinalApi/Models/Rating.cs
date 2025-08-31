using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MehnahFinalApi.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public Work? Work { get; set; }

        // تغيير اسم الحقل لتجنب التعارض
        [Required]
        [ForeignKey("Reviewer")]
        public int? ReviewerId { get; set; } // كان UserId

        public User? Reviewer { get; set; } // كان User

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

