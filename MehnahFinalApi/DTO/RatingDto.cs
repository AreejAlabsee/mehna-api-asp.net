using System.ComponentModel.DataAnnotations;

namespace MehnahFinalApi.DTO
{
    public class RatingDto
    {

        [Range(1, 5)]
        public int Stars { get; set; }

        public string? Comment { get; set; }

        public int WorkId { get; set; }
        public string? ReviewerName { get; set; }
        public int ReviewerId { get; set; }
    }

}

