using MehnahFinalApi.DTO;
using MehnahFinalMVC.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MehnahFinalMVC.Services
{
    public class WorksApiService
    {
        private readonly HttpClient _client;

        public WorksApiService(HttpClient client)
        {
            _client = client;
        }

            // ✅ جلب كل الأعمال مع تحويل من DTO إلى ViewModel
            public async Task<List<WorksViewModel>> GetAllWorksAsync()
            {
                var dtos = await _client.GetFromJsonAsync<List<WorkDto>>("Works");

                if (dtos == null) return new List<WorksViewModel>();

                return dtos.Select(dto => new WorksViewModel
                {
                    Id = dto.Id,
                    Description = dto.Description,
                    ContactMethod = dto.ContactMethod,
                    Category = dto.Category,
                    IsAvailable = dto.IsAvailable,
                    UserId = dto.UserId,
                    Price = dto.Price
                }).ToList();
            }
        
    


    // جلب عمل واحد حسب الـ Id
    public async Task<WorksViewModel> GetWorkByIdAsync(int id)
        {
            var work = await _client.GetFromJsonAsync<WorksViewModel>($"Works/{id}");
            return work;
        }

        // إنشاء عمل جديد
        public async Task<bool> AddWorkAsync(WorksViewModel work)
        {
            var response = await _client.PostAsJsonAsync("Works", work);
            return response.IsSuccessStatusCode;
        }

        // تحديث عمل موجود
        public async Task<bool> UpdateWorkAsync(int id, WorksViewModel work)
        {
            var response = await _client.PutAsJsonAsync($"Works/{id}", work);
            return response.IsSuccessStatusCode;
        }

        // حذف عمل
        public async Task<bool> DeleteWorkAsync(int id)
        {
            var response = await _client.DeleteAsync($"Works/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
