using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MehnahFinalMVC.ViewModels
{
    public class WorkerViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, StringLength(10)]
        public string UserType { get; set; } = "worker";

        // فقط للرفع من الفورم
        [JsonIgnore]
        public string ProfileImage { get; set; }

        // فقط للعرض من API
        public string ProfileImageUrl { get; set; }
    }
}
