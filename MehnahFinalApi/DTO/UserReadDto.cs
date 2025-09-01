namespace MehnahFinalApi.DTO
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; } // ✅ هنا فقط
    }
}
