using MehnahFinalApi.DTO;

namespace MehnahFinalMVC.ViewModels
{
    public class WorkDetailsViewModel
    {
        public WorkDto Work { get; set; }
        public List<RatingDto> Ratings { get; set; }
    }
}