using System.ComponentModel.DataAnnotations;


namespace MehnahFinalApi.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }



      public IFormFile? ProfileImage { get; set; } // ✅ هنا فقط

        public string? ProfileImageUrl { get; set; } // ✅ هنا فقط




    }

}

