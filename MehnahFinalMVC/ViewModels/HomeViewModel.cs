using MehnahFinalApi.DTO;

namespace MehnahFinalMVC.ViewModels
{
    public class HomeViewModel
    {
        // قائمة الأعمال (العمال)
        public List<WorkDto> Works { get; set; }

        // قائمة المستخدمين
        public List<UserDto> Users { get; set; }



        // قائمة التقييمات
        public List<RatingResponseDto> Ratings { get; set; } 

    }
}
