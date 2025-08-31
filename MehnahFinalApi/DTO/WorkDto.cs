using System.ComponentModel.DataAnnotations;

namespace MehnahFinalApi.DTO
{
    public class WorkDto
    {
        public int Id { get; set; }

        public bool IsAvailable { get; set; }  // تم تعديلها إلى Property

        [Required]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string Category { get; set; }

        [Required]
        public int UserId { get; set; }

        public string ContactMethod { get; set; }

        [Required] // يمكن جعله nullable عند التحديث
        public IFormFile? WorkImages { get; set; }

        public string? WorkImagesUrl { get; set; }

    }
}

