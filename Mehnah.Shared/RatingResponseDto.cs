namespace Mehnah.Shared
{
    internal class RatingResponseDto
    {

        public int Id { get; set; }
        public int Stars { get; set; }
        public string? Comment { get; set; }
        public string? ReviewerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int WorkId { get; set; }

    }
}
