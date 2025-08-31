using MehnahFinalMVC.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MehnahFinalMVC.Services
{
    public class RatingsApiService
    {
        private readonly HttpClient _client;

        public RatingsApiService(HttpClient client)
        {
            _client = client;
        }

        // ✅ جلب كل التقييمات
        public async Task<List<RatingViewModel>> GetAllRatingsAsync()
        {
            var ratings = await _client.GetFromJsonAsync<List<RatingViewModel>>("Ratings");
            return ratings ?? new List<RatingViewModel>();
        }

        // ✅ جلب تقييم واحد حسب الـ Id
        public async Task<RatingViewModel?> GetRatingByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<RatingViewModel>($"Ratings/{id}");
        }

        // ✅ إضافة تقييم جديد
        public async Task<bool> AddRatingAsync(RatingViewModel rating)
        {
            var response = await _client.PostAsJsonAsync("Ratings", rating);
            return response.IsSuccessStatusCode;
        }

        // ✅ تعديل تقييم موجود
        public async Task<bool> UpdateRatingAsync(int id, RatingViewModel rating)
        {
            var response = await _client.PutAsJsonAsync($"Ratings/{id}", rating);
            return response.IsSuccessStatusCode;
        }

        // ✅ حذف تقييم
        public async Task<bool> DeleteRatingAsync(int id)
        {
            var response = await _client.DeleteAsync($"Ratings/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
