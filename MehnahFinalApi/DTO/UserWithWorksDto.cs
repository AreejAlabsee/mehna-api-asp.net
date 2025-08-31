namespace MehnahFinalApi.DTO
{
    public class UserWithWorksDto
    {
        public UserReadDto User { get; set; }

        public List<WorkDto> Works { get; set; } = new();
    }
}
