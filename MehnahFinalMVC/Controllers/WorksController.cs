
//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorksController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public WorksController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("https://192.168.1.104:7232/api/");
//        }

//        public async Task<IActionResult> Index()
//        {
//            var response = await _httpClient.GetAsync("Works");

//            // إذا لم ينجح الطلب، قم بإنشاء ViewModel فارغ
//            if (!response.IsSuccessStatusCode)
//            {
//                return View(new WorksViewModel());
//            }

//            // قراءة البيانات من الرد
//            var works = await response.Content.ReadFromJsonAsync<List<WorkDto>>();

//            // إنشاء ViewModel جديد وتعبئته بالبيانات
//            var viewModel = new HomeViewModel
//            {
//                Works = works
//            };

//            // إرجاع الـ ViewModel الصحيح إلى العرض
//            return View(viewModel);
//        }
//        // ✅ 2. عرض التفاصيل
//        public async Task<IActionResult> Details(int id)
//        {
//            var workResponse = await _httpClient.GetAsync($"Works/{id}");
//            var ratingsResponse = await _httpClient.GetAsync($"Ratings/ByWorkId/{id}");

//            if (!workResponse.IsSuccessStatusCode || !ratingsResponse.IsSuccessStatusCode)
//                return NotFound();

//            var work = await workResponse.Content.ReadFromJsonAsync<WorkDto>();
//            var ratings = await ratingsResponse.Content.ReadFromJsonAsync<List<RatingDto>>();

//            var vm = new WorkDetailsViewModel
//            {
//                Work = work,
//                Ratings = ratings
//            };

//            return View(vm);
//        }

//        // ✅ 3. إنشاء عمل جديد
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(WorkDto dto, IFormFile WorkImages)
//        {
//            var form = new MultipartFormDataContent
//            {
//                { new StringContent(dto.Description ?? ""), "Description" },
//                { new StringContent(dto.Category ?? ""), "Category" },
//                { new StringContent(dto.Price.ToString()), "Price" },
//                { new StringContent(dto.UserId.ToString()), "UserId" },
//                { new StringContent(dto.ContactMethod ?? ""), "ContactMethod" },
//                { new StringContent(dto.IsAvailable.ToString()), "IsAvailable" }
//            };

//            if (WorkImages != null && WorkImages.Length > 0)
//            {
//                var streamContent = new StreamContent(WorkImages.OpenReadStream());
//                streamContent.Headers.ContentType = new MediaTypeHeaderValue(WorkImages.ContentType);
//                form.Add(streamContent, "WorkImages", WorkImages.FileName);
//            }

//            var response = await _httpClient.PostAsync("Works", form);
//            if (response.IsSuccessStatusCode)
//                return RedirectToAction("Index");

//            return View(dto);
//        }

//        // ✅ 4. تعديل
//        public async Task<IActionResult> Edit(int id)
//        {
//            var response = await _httpClient.GetAsync($"Works/{id}");
//            if (!response.IsSuccessStatusCode) return NotFound();

//            var work = await response.Content.ReadFromJsonAsync<WorkDto>();
//            return View(work);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, WorkDto dto, IFormFile WorkImages)
//        {
//            var form = new MultipartFormDataContent
//            {
//                { new StringContent(dto.Description ?? ""), "Description" },
//                { new StringContent(dto.Category ?? ""), "Category" },
//                { new StringContent(dto.Price.ToString()), "Price" },
//                { new StringContent(dto.UserId.ToString()), "UserId" },
//                { new StringContent(dto.ContactMethod ?? ""), "ContactMethod" },
//                { new StringContent(dto.IsAvailable.ToString()), "IsAvailable" }
//            };

//            if (WorkImages != null && WorkImages.Length > 0)
//            {
//                var streamContent = new StreamContent(WorkImages.OpenReadStream());
//                streamContent.Headers.ContentType = new MediaTypeHeaderValue(WorkImages.ContentType);
//                form.Add(streamContent, "WorkImages", WorkImages.FileName);
//            }

//            var response = await _httpClient.PutAsync($"Works/{id}", form);
//            if (response.IsSuccessStatusCode)
//                return RedirectToAction("Index");

//            return View(dto);
//        }

//        // ✅ 5. حذف
//        public async Task<IActionResult> Delete(int id)
//        {
//            var response = await _httpClient.GetAsync($"Works/{id}");
//            if (!response.IsSuccessStatusCode) return NotFound();

//            var work = await response.Content.ReadFromJsonAsync<WorkDto>();
//            return View(work);
//        }

//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"Works/{id}");
//            return RedirectToAction("Index");
//        }
//    }
////}
//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Json;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorksController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public WorksController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("https://192.168.1.104:7232/api/");
//        }

//        [HttpGet]
//        // GET: Works
//        public async Task<IActionResult> Index()
//        {
//            var response = await _httpClient.GetAsync("Works");

//            if (!response.IsSuccessStatusCode)
//                return View(new HomeViewModel { Works = new List<WorkDto>() });

//            var works = await response.Content.ReadFromJsonAsync<List<WorkDto>>();

//            var model = new HomeViewModel
//            {
//                Works = works ?? new List<WorkDto>()
//            };

//            return View(works);
//        }

//        [HttpGet]
//        // GET: Works/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Works/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
//                return View(work);
//            }

//            return NotFound();
//        }

//        [HttpGet]
//        // GET: Works/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([FromForm] WorkDto workDto)
//        {
//            if (ModelState.IsValid)
//            {
//                var response = await _httpClient.PostAsJsonAsync("Works", workDto);
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(workDto);
//        }

//        [HttpGet]
//        // GET: Works/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Works/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
//                return View(work);
//            }

//            return NotFound();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [FromForm] WorkDto workDto)
//        {
//            if (id != workDto.Id) return NotFound();

//            if (ModelState.IsValid)
//            {
//                var response = await _httpClient.PutAsJsonAsync($"Works/{id}", workDto);
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }
//            }
//            return View(workDto);
//        }

//        [HttpGet]
//        // GET: Works/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Works/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
//                return View(work);
//            }

//            return NotFound();
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"Works/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                return RedirectToAction(nameof(Index));
//            }

//            return BadRequest();
//        }
//    }
//}




using MehnahFinalApi.DTO;
using MehnahFinalMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MehnahFinalMVC.Controllers
{
    public class WorksController : Controller
    {
        private readonly HttpClient _httpClient;

        public WorksController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Works");

            if (!response.IsSuccessStatusCode)
            {
                return View(new HomeViewModel { Works = new List<WorkDto>() });
            }

            var works = await response.Content.ReadFromJsonAsync<List<WorkDto>>();

            var model = new HomeViewModel
            {
                Works = works ?? new List<WorkDto>()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"Works/{id}");
            if (response.IsSuccessStatusCode)
            {
                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
                return View(work);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkDto work)
        {
            if (ModelState.IsValid)
            {
                using var formContent = new MultipartFormDataContent();

                // البيانات النصية
                formContent.Add(new StringContent(work.Description ?? ""), "Description");
                formContent.Add(new StringContent(work.Price?.ToString() ?? ""), "Price");
                formContent.Add(new StringContent(work.Category ?? ""), "Category");
                formContent.Add(new StringContent(work.UserId.ToString()), "UserId");
                formContent.Add(new StringContent(work.ContactMethod ?? ""), "ContactMethod");
                formContent.Add(new StringContent(work.IsAvailable.ToString()), "IsAvailable");

                // رفع الصورة لو موجودة
                if (work.WorkImages != null && work.WorkImages.Length > 0)
                {
                    var stream = work.WorkImages.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(work.WorkImages.ContentType);
                    formContent.Add(fileContent, "WorkImages", work.WorkImages.FileName);
                }

                var response = await _httpClient.PostAsync("Works", formContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(work);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"Works/{id}");
            if (response.IsSuccessStatusCode)
            {
                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
                return View(work);
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkDto work)
        {
            if (id != work.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(work);
            }

            try
            {
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent(work.Id.ToString()), nameof(work.Id));
                form.Add(new StringContent(work.Description ?? ""), nameof(work.Description));
                form.Add(new StringContent(work.Price?.ToString() ?? ""), nameof(work.Price));
                form.Add(new StringContent(work.Category ?? ""), nameof(work.Category));
                form.Add(new StringContent(work.UserId.ToString()), nameof(work.UserId));
                form.Add(new StringContent(work.ContactMethod ?? ""), nameof(work.ContactMethod));
                form.Add(new StringContent(work.IsAvailable.ToString()), nameof(work.IsAvailable));

                if (work.WorkImages != null && work.WorkImages.Length > 0)
                {
                    var stream = work.WorkImages.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(work.WorkImages.ContentType);
                    form.Add(fileContent, nameof(work.WorkImages), work.WorkImages.FileName);
                }

                var response = await _httpClient.PutAsync($"Works/{id}", form);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating work: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Exception: " + ex.Message);
            }

            return View(work);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"Works/{id}");
            if (response.IsSuccessStatusCode)
            {
                var work = await response.Content.ReadFromJsonAsync<WorkDto>();
                return View(work);
            }

            return NotFound();
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Works/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error deleting work: " + response.ReasonPhrase);
            // يمكن إعادة العمل نفسه إذا فشل الحذف
            var workResponse = await _httpClient.GetAsync($"Works/{id}");
            var work = await workResponse.Content.ReadFromJsonAsync<WorkDto>();
            return View("Delete", work);
        }

    }
}
