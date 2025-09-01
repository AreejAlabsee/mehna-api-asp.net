using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace MehnahFinalApi.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }




        public string? ProfileImageUrl { get; set; } // ✅ هنا فقط

        
        [Required]
        [JsonIgnore]
        public IFormFile? ProfileImage { get; set; } // ✅ هنا فقط


    }

}

