using System.ComponentModel.DataAnnotations;

namespace MehnahFinalApi.Models
{
    public class User
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }



        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [StringLength(10)]
        public string? UserType { get; set; } // "Worker" or "Client"

        public string ProfileImage { get; set; }



        // علاقات التنقل والربط داخل قواعد البيانات حيث تسهل انشاء الجداول وعمليات الربط بينهم
        public ICollection<Work> Works { get; set; }
        public ICollection<Rating> RatingsGiven { get; set; } = new List<Rating>();

    }
}

