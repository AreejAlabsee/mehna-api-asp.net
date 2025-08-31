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

        // ✅ 1. عرض قائمة الأعمال
        //public async Task<IActionResult> Index()
        //{
        //    var response = await _httpClient.GetAsync("Works");
        //    if (!response.IsSuccessStatusCode) return View(new List<WorkDto>());

        //    var works = await response.Content.ReadFromJsonAsync<List<WorkDto>>();
        //    return View(works);
        //}
        // داخل الـ Controller
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Works");

            // إذا لم ينجح الطلب، قم بإنشاء ViewModel فارغ
            if (!response.IsSuccessStatusCode)
            {
                return View(new HomeViewModel());
            }

            // قراءة البيانات من الرد
            var works = await response.Content.ReadFromJsonAsync<List<WorkDto>>();

            // إنشاء ViewModel جديد وتعبئته بالبيانات
            var viewModel = new HomeViewModel
            {
                Works = works
            };

            // إرجاع الـ ViewModel الصحيح إلى العرض
            return View(viewModel);
        }
        // ✅ 2. عرض التفاصيل
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

        // ✅ 4. تعديل
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

        // ✅ 5. حذف
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
            var response = await _httpClient.DeleteAsync($"Works/{id}");
            return RedirectToAction("Index");
        }
    }
}
