using MehnahFinalApi.DTO;
using MehnahFinalMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace MehnahFinalMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
        }

        // ✅ 1. عرض قائمة الأعمال + التقييمات (بشكل منفصل)
        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();

            // جلب الأعمال
            var worksResponse = await _httpClient.GetAsync("Works");
            if (worksResponse.IsSuccessStatusCode)
            {
                vm.Works = await worksResponse.Content.ReadFromJsonAsync<List<WorkDto>>();
            }

            // جلب التقييمات
            var ratingsResponse = await _httpClient.GetAsync("Ratings");
            if (ratingsResponse.IsSuccessStatusCode)
            {
                vm.Ratings = await ratingsResponse.Content.ReadFromJsonAsync<List<RatingResponseDto>>();
            }

            // جلب العمال
            var workersResponse = await _httpClient.GetAsync("Users");
            if (workersResponse.IsSuccessStatusCode)
            {
                vm.Users = await workersResponse.Content.ReadFromJsonAsync<List<UserDto>>();
                // ✅ سيقرأ ProfileImageUrl فقط
            }


            return View(vm);
        }


        // ✅ 2. عرض تفاصيل عمل معين مع تقييماته
        public async Task<IActionResult> Details(int id)
        {
            var workResponse = await _httpClient.GetAsync($"Works/{id}");
            var ratingsResponse = await _httpClient.GetAsync($"Ratings/ByWorkId/{id}");

            if (!workResponse.IsSuccessStatusCode || !ratingsResponse.IsSuccessStatusCode)
                return NotFound();

            var work = await workResponse.Content.ReadFromJsonAsync<WorkDto>();
            var ratings = await ratingsResponse.Content.ReadFromJsonAsync<List<RatingDto>>();

            var vm = new WorkDetailsViewModel
            {
                Work = work,
                Ratings = ratings
            };

            return View(vm);
        }

        // ✅ 3. إنشاء عمل جديد
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkDto dto, IFormFile WorkImages)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.Category ?? ""), "Category" },
                { new StringContent(dto.Price.ToString()), "Price" },
                { new StringContent(dto.UserId.ToString()), "UserId" },
                { new StringContent(dto.ContactMethod ?? ""), "ContactMethod" },
                { new StringContent(dto.IsAvailable.ToString()), "IsAvailable" }
            };

            if (WorkImages != null && WorkImages.Length > 0)
            {
                var streamContent = new StreamContent(WorkImages.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(WorkImages.ContentType);
                form.Add(streamContent, "WorkImages", WorkImages.FileName);
            }

            var response = await _httpClient.PostAsync("Works", form);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        // ✅ 4. تعديل عمل
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Works/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var work = await response.Content.ReadFromJsonAsync<WorkDto>();
            return View(work);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WorkDto dto, IFormFile WorkImages)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(dto.Description ?? ""), "Description" },
                { new StringContent(dto.Category ?? ""), "Category" },
                { new StringContent(dto.Price.ToString()), "Price" },
                { new StringContent(dto.UserId.ToString()), "UserId" },
                { new StringContent(dto.ContactMethod ?? ""), "ContactMethod" },
                { new StringContent(dto.IsAvailable.ToString()), "IsAvailable" }
            };

            if (WorkImages != null && WorkImages.Length > 0)
            {
                var streamContent = new StreamContent(WorkImages.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(WorkImages.ContentType);
                form.Add(streamContent, "WorkImages", WorkImages.FileName);
            }

            var response = await _httpClient.PutAsync($"Works/{id}", form);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        // ✅ 5. حذف عمل
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"Works/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var work = await response.Content.ReadFromJsonAsync<WorkDto>();
            return View(work);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"Works/{id}");
            return RedirectToAction("Index");
        }
    }
}
