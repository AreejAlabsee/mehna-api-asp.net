//////using Microsoft.AspNetCore.Mvc;
//////using MehnahFinalMVC.Services;
//////using MehnahFinalMVC.ViewModels;

//////namespace MehnahFinalMVC.Controllers
//////{
//////    public class WorkerController : Controller
//////    {
//////        private readonly WorkerApiService _client;

//////        public WorkerController(WorkerApiService client)
//////        {
//////            _client = client;
//////        }

//////        // GET: Worker
//////        public async Task<IActionResult> Index()
//////        {
//////            var workers = await _client.GetAllWorkersAsync();
//////            return View(workers);
//////        }

//////        // GET: Worker/Details/5
//////        public async Task<IActionResult> Details(int id)
//////        {
//////            var worker = await _client.GetWorkerAsync(id);
//////            if (worker == null) return NotFound();
//////            return View(worker);
//////        }

//////        // GET: Worker/Create
//////        public IActionResult Create() => View();

//////        // POST: Worker/Create
//////        [HttpPost]
//////        [ValidateAntiForgeryToken]
//////        public async Task<IActionResult> Create(WorkerViewModel worker)
//////        {
//////            if (!ModelState.IsValid) return View(worker);

//////            var success = await _client.AddWorkerAsync(worker);
//////            if (success) return RedirectToAction(nameof(Index));

//////            ModelState.AddModelError("", "Failed to create worker.");
//////            return View(worker);
//////        }

//////        // GET: Worker/Edit/5
//////        public async Task<IActionResult> Edit(int id)
//////        {
//////            var worker = await _client.GetWorkerAsync(id);
//////            if (worker == null) return NotFound();
//////            return View(worker);
//////        }

//////        // POST: Worker/Edit/5
//////        [HttpPost]
//////        [ValidateAntiForgeryToken]
//////        public async Task<IActionResult> Edit(int id, WorkerViewModel worker)
//////        {
//////            if (id != worker.Id) return NotFound();
//////            if (!ModelState.IsValid) return View(worker);

//////            var success = await _client.UpdateWorkerAsync(id, worker);
//////            if (success) return RedirectToAction(nameof(Index));

//////            ModelState.AddModelError("", "Failed to update worker.");
//////            return View(worker);
//////        }

//////        // GET: Worker/Delete/5
//////        public async Task<IActionResult> Delete(int id)
//////        {
//////            var worker = await _client.GetWorkerAsync(id);
//////            if (worker == null) return NotFound();
//////            return View(worker);
//////        }

//////        // POST: Worker/Delete/5
//////        [HttpPost, ActionName("Delete")]
//////        [ValidateAntiForgeryToken]
//////        public async Task<IActionResult> DeleteConfirmed(int id)
//////        {
//////            var success = await _client.DeleteWorkerAsync(id);
//////            if (success) return RedirectToAction(nameof(Index));

//////            ModelState.AddModelError("", "Failed to delete worker.");
//////            var worker = await _client.GetWorkerAsync(id);
//////            return View("Delete", worker);
//////        }
//////    }
//////}



////using MehnahFinalApi.DTO;
////using MehnahFinalMVC.ViewModels;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.ViewFeatures;
////using System.Net.Http.Headers;
////using System.Net.Http.Json;

////namespace MehnahFinalMVC.Controllers
////{
////    public class WorkerController : Controller
////    {
////        private readonly HttpClient _httpClient;

////        public WorkerController(HttpClient httpClient)
////        {
////            _httpClient = httpClient;
////            _httpClient.BaseAddress = new Uri("https://192.168.1.104:7232/api/");
////        }
////        //public async Task<IActionResult> Index()
////        //{
////        //    var response = await _httpClient.GetAsync("Users");

////        //    var workers = new List<UserDto>();
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        workers = await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? new List<UserDto>();
////        //    }

////        //    return View(workers);   // <-- هنا يرجع List
////        //}

////        //[HttpGet]
////        //public async Task<IActionResult> Index()
////        //{
////        //    var response = await _httpClient.GetAsync("Users");

////        //    if (!response.IsSuccessStatusCode)
////        //    {
////        //        return View(new HomeViewModel { Users = new List<UserDto>() });
////        //    }

////        //    var worker = await response.Content.ReadFromJsonAsync<List<UserDto>>();

////        //    var model = new HomeViewModel
////        //    {
////        //        Users = worker ?? new List<UserDto>()
////        //    };

////        //    return View(model);
////        //}

////        [HttpGet]
////        public async Task<IActionResult> Index()
////        {
////            var response = await _httpClient.GetAsync("Users");

////            if (!response.IsSuccessStatusCode)
////            {
////                return View(new HomeViewModel { Users = new List<UserDto>() });
////            }

////            var workers = await response.Content.ReadFromJsonAsync<List<UserDto>>();

////            var model = new HomeViewModel
////            {
////                Users = workers ?? new List<UserDto>()
////            };

////            return View(model);
////        }

////        [HttpGet]
////        public async Task<IActionResult> Details(int? id)
////        {
////            if (id == null) return NotFound();

////            var response = await _httpClient.GetAsync($"Users/{id}");
////            if (response.IsSuccessStatusCode)
////            {
////                var wirker = await response.Content.ReadFromJsonAsync<UserDto>();
////                return View(wirker);
////            }

////            return NotFound();
////        }

////        [HttpGet]
////        public IActionResult Create()
////        {
////            return View();
////        }

////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public async Task<IActionResult> Create(UserDto worker)
////        {
////            if (ModelState.IsValid)
////            {
////                using var formContent = new MultipartFormDataContent();

////                // البيانات النصية
////                formContent.Add(new StringContent(worker.Name ?? ""), "Name");
////                formContent.Add(new StringContent(worker.Password ?? ""), "Password");
////                formContent.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
////                formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

////                // رفع الصورة لو موجودة
////                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
////                {
////                    var stream = worker.ProfileImage.OpenReadStream();
////                    var fileContent = new StreamContent(stream);
////                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);

////                    // مهم: اسم البراميتر هنا لازم يطابق اسم البراميتر اللي يستقبله الـAPI
////                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
////                }

////                var response = await _httpClient.PostAsync("Users", formContent);

////                if (response.IsSuccessStatusCode)
////                {
////                    return RedirectToAction(nameof(Index));
////                }
////            }

////            return View(worker);
////        }

////        //[HttpPost]
////        //[ValidateAntiForgeryToken]
////        //public async Task<IActionResult> Create(UserReadDto worker)
////        //{
////        //    if (ModelState.IsValid)
////        //    {
////        //        using var formContent = new MultipartFormDataContent();

////        //        // البيانات النصية
////        //        formContent.Add(new StringContent(worker.Name ?? ""), "Name");
////        //        formContent.Add(new StringContent(worker.Password ?? ""), "Password");
////        //        formContent.Add(new StringContent(worker.PhoneNumber.ToString()), "PhoneNumber");
////        //        formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

////        //        // رفع الصورة لو موجودة
////        //        if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
////        //        {
////        //            var stream = worker.ProfileImage.OpenReadStream();
////        //            var fileContent = new StreamContent(stream);
////        //            fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
////        //            formContent.Add(fileContent, "WorkImages", worker.ProfileImage.FileName);
////        //        }

////        //        var response = await _httpClient.PostAsync("Users", formContent);

////        //        if (response.IsSuccessStatusCode)
////        //        {
////        //            return RedirectToAction(nameof(Index));
////        //        }
////        //    }

////        //    return View(User);
////        //}

////        //[HttpGet]
////        //public async Task<IActionResult> Edit(int? id)
////        //{
////        //    if (id == null) return NotFound();

////        //    var response = await _httpClient.GetAsync($"Users/{id}");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
////        //        return View(worker);
////        //    }

////        //    return NotFound();
////        //}
////        [HttpGet]
////        public async Task<IActionResult> Edit(int? id)
////        {
////            if (id == null) return NotFound();

////            var response = await _httpClient.GetAsync($"Users/{id}");
////            if (response.IsSuccessStatusCode)
////            {
////                var worker = await response.Content.ReadFromJsonAsync<UserDto>();
////                return View(worker);
////            }

////            return NotFound();
////        }

////        //[HttpPost]
////        //[ValidateAntiForgeryToken]
////        //public async Task<IActionResult> Edit(int id, UserReadDto worker)
////        //{
////        //    if (id != worker.Id) return NotFound();

////        //    if (!ModelState.IsValid)
////        //    {
////        //        return View(worker);
////        //    }

////        //    try
////        //    {
////        //        using var form = new MultipartFormDataContent();

////        //        form.Add(new StringContent(worker.Id.ToString()), nameof(worker.Id));
////        //        form.Add(new StringContent(worker.Name ?? ""), nameof(worker.Name));
////        //        form.Add(new StringContent(worker.Password ?? ""), nameof(worker.Password));
////        //        form.Add(new StringContent(worker.UserType.ToString()), nameof(worker.UserType));
////        //        form.Add(new StringContent(worker.PhoneNumber ?? ""), nameof(worker.PhoneNumber));

////        //        if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
////        //        {
////        //            var stream = worker.ProfileImage.OpenReadStream();
////        //            var fileContent = new StreamContent(stream);
////        //            fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
////        //            form.Add(fileContent, nameof(worker.ProfileImage), worker.ProfileImage.FileName);
////        //        }

////        //        var response = await _httpClient.PutAsync($"Users/{id}", form);

////        //        if (response.IsSuccessStatusCode)
////        //        {
////        //            return RedirectToAction("Index");
////        //        }
////        //        else
////        //        {
////        //            ModelState.AddModelError(string.Empty, "Error updating work: " + response.ReasonPhrase);
////        //        }
////        //    }
////        //    catch (Exception ex)
////        //    {
////        //        ModelState.AddModelError(string.Empty, "Exception: " + ex.Message);
////        //    }

////        //    return View(worker);
////        //}

////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public async Task<IActionResult> Edit(int id, UserDto worker)
////        {
////            if (id != worker.Id) return NotFound();
////            if (!ModelState.IsValid) return View(worker);

////            using var form = new MultipartFormDataContent();
////            form.Add(new StringContent(worker.Id.ToString()), "Id");
////            form.Add(new StringContent(worker.Name ?? ""), "Name");
////            form.Add(new StringContent(worker.Password ?? ""), "Password");
////            form.Add(new StringContent(worker.UserType ?? ""), "UserType");
////            form.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");

////            if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
////            {
////                var stream = worker.ProfileImage.OpenReadStream();
////                var fileContent = new StreamContent(stream);
////                fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
////                form.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
////            }

////            var response = await _httpClient.PutAsync($"Users/{id}", form);

////            if (response.IsSuccessStatusCode)
////                return RedirectToAction(nameof(Index));

////            ModelState.AddModelError("", "Error updating worker");
////            return View(worker);
////        }


////        [HttpGet]
////        public async Task<IActionResult> Delete(int? id)
////        {
////            if (id == null) return NotFound();

////            var response = await _httpClient.GetAsync($"Users/{id}");
////            if (response.IsSuccessStatusCode)
////            {
////                var worker = await response.Content.ReadFromJsonAsync<UserDto>();
////                return View(worker);
////            }

////            return NotFound();
////        }

////        [HttpPost, ActionName("DeleteConfirmed")]
////        [ValidateAntiForgeryToken]
////        public async Task<IActionResult> DeleteConfirmed(int id)
////        {
////            var response = await _httpClient.DeleteAsync($"Users/{id}");
////            if (response.IsSuccessStatusCode)
////            {
////                return RedirectToAction(nameof(Index));
////            }

////            ModelState.AddModelError(string.Empty, "Error deleting work: " + response.ReasonPhrase);
////            // يمكن إعادة العمل نفسه إذا فشل الحذف
////            var workResponse = await _httpClient.GetAsync($"Users/{id}");
////            var worker = await workResponse.Content.ReadFromJsonAsync<UserDto>();
////            return View("Delete", worker);
////        }

////    }
//}
//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorkerController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public WorkerController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
//        }

//        // GET: Worker
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var response = await _httpClient.GetAsync("Users");

//            if (!response.IsSuccessStatusCode)
//            {
//                return View(new HomeViewModel { Users = new List<UserDto>() });
//            }

//            var works = await response.Content.ReadFromJsonAsync<List<UserDto>>();

//            var model = new HomeViewModel
//            {
//                Users = works ?? new List<UserDto>()
//            };

//            return View(model);
//        }

//        // GET: Worker/Details/5
//        [HttpGet]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        // GET: Worker/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Worker/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(UserDto worker)
//        {
//            if (ModelState.IsValid)
//            {
//                using var formContent = new MultipartFormDataContent();

//                formContent.Add(new StringContent(worker.Name ?? ""), "Name");
//                formContent.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
//                formContent.Add(new StringContent(worker.Password ?? ""), "Password");
//                formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PostAsync("Users", formContent);

//                if (response.IsSuccessStatusCode)
//                    return RedirectToAction(nameof(Index));
//            }

//            return View(worker);
//        }

//        // GET: Worker/Edit/5
//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        // POST: Worker/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, UserDto worker)
//        {
//            if (id != worker.Id) return NotFound();

//            if (!ModelState.IsValid)
//                return View(worker);

//            using var form = new MultipartFormDataContent();

//            form.Add(new StringContent(worker.Id.ToString()), nameof(worker.Id));
//            form.Add(new StringContent(worker.Name ?? ""), nameof(worker.Name));
//            form.Add(new StringContent(worker.PhoneNumber ?? ""), nameof(worker.PhoneNumber));
//            form.Add(new StringContent(worker.Password ?? ""), nameof(worker.Password));
//            form.Add(new StringContent(worker.UserType ?? ""), nameof(worker.UserType));

//            if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//            {
//                var stream = worker.ProfileImage.OpenReadStream();
//                var fileContent = new StreamContent(stream);
//                fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                form.Add(fileContent, nameof(worker.ProfileImage), worker.ProfileImage.FileName);
//            }

//            var response = await _httpClient.PutAsync($"Users/{id}", form);

//            if (response.IsSuccessStatusCode)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError(string.Empty, "Error updating worker.");
//            return View(worker);
//        }

//        // GET: Worker/Delete/5
//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        // POST: Worker/Delete/5
//        [HttpPost, ActionName("DeleteConfirmed")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError(string.Empty, "Error deleting worker.");
//            var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//            var worker = await workerResponse.Content.ReadFromJsonAsync<UserDto>();
//            return View("Delete", worker);
//        }
//    }
//}


//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorkerController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public WorkerController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("https://192.168.1.104:7232/api/");
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var response = await _httpClient.GetAsync("Users");

//            if (!response.IsSuccessStatusCode)
//            {
//                return View(new HomeViewModel { Users = new List<UserDto>() });
//            }

//            var works = await response.Content.ReadFromJsonAsync<List<UserDto>>();

//            var model = new HomeViewModel
//            {
//                Users = works ?? new List<UserDto>()
//            };

//            return View(model);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }
//        // POST: Worker/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(UserDto worker)
//        {
//            if (ModelState.IsValid)
//            {
//                using var formContent = new MultipartFormDataContent();

//                formContent.Add(new StringContent(worker.Name ?? ""), "Name");
//                formContent.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
//                formContent.Add(new StringContent(worker.Password ?? ""), "Password");
//                formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PostAsync("Users", formContent);

//                if (response.IsSuccessStatusCode)
//                    return RedirectToAction(nameof(Index));
//            }

//            return View(worker);
//        }

//        // GET: Worker/Edit/5
//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        // POST: Worker/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, UserDto worker)
//        {
//            if (id != worker.Id) return NotFound();

//            if (!ModelState.IsValid)
//                return View(worker);

//            using var form = new MultipartFormDataContent();

//            form.Add(new StringContent(worker.Id.ToString()), nameof(worker.Id));
//            form.Add(new StringContent(worker.Name ?? ""), nameof(worker.Name));
//            form.Add(new StringContent(worker.PhoneNumber ?? ""), nameof(worker.PhoneNumber));
//            form.Add(new StringContent(worker.Password ?? ""), nameof(worker.Password));
//            form.Add(new StringContent(worker.UserType ?? ""), nameof(worker.UserType));

//            if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//            {
//                var stream = worker.ProfileImage.OpenReadStream();
//                var fileContent = new StreamContent(stream);
//                fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                form.Add(fileContent, nameof(worker.ProfileImage), worker.ProfileImage.FileName);
//            }

//            var response = await _httpClient.PutAsync($"Users/{id}", form);

//            if (response.IsSuccessStatusCode)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError(string.Empty, "Error updating worker.");
//            return View(worker);
//        }

//        // GET: Worker/Delete/5
//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            var response = await _httpClient.GetAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//            {
//                var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                return View(worker);
//            }

//            return NotFound();
//        }

//        // POST: Worker/Delete/5
//        [HttpPost, ActionName("DeleteConfirmed")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var response = await _httpClient.DeleteAsync($"Users/{id}");
//            if (response.IsSuccessStatusCode)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError(string.Empty, "Error deleting worker.");
//            var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//            var worker = await workerResponse.Content.ReadFromJsonAsync<UserDto>();
//            return View("Delete", worker);
//        }
//    }
////}using MehnahFinalApi.DTO;
//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Text;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorkerController : Controller
//    {
//        private readonly HttpClient _httpClient;
//        private readonly ILogger<WorkerController> _logger;

//        public WorkerController(HttpClient httpClient, ILogger<WorkerController> logger)
//        {
//            _httpClient = httpClient;
//            _logger = logger;
//            _httpClient.BaseAddress = new Uri("https://192.168.1.104:7232/api/");
//            _httpClient.Timeout = TimeSpan.FromSeconds(30);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching workers from API...");
//                var response = await _httpClient.GetAsync("Users");

//                if (!response.IsSuccessStatusCode)
//                {
//                    _logger.LogWarning("Failed to fetch workers. Status: {StatusCode}", response.StatusCode);
//                    return View(new HomeViewModel { Users = new List<UserDto>() });
//                }

//                var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
//                _logger.LogInformation("Successfully fetched {Count} workers", users?.Count ?? 0);

//                var model = new HomeViewModel
//                {
//                    Users = users ?? new List<UserDto>()
//                };

//                return View(model);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading workers");
//                ModelState.AddModelError("", "Error loading workers: " + ex.Message);
//                return View(new HomeViewModel { Users = new List<UserDto>() });
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                    return View(worker);
//                }

//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker details for ID {Id}", id);
//                ModelState.AddModelError("", "Error loading details");
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(UserDto worker)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(worker);
//            }

//            try
//            {
//                using var formContent = new MultipartFormDataContent();

//                // Add text data
//                formContent.Add(new StringContent(worker.Name ?? "", Encoding.UTF8), "Name");
//                formContent.Add(new StringContent(worker.PhoneNumber ?? "", Encoding.UTF8), "PhoneNumber");
//                formContent.Add(new StringContent(worker.Password ?? "", Encoding.UTF8), "Password");
//                formContent.Add(new StringContent(worker.UserType ?? "", Encoding.UTF8), "UserType");

//                // Add image if exists
//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PostAsync("Users", formContent);

//                if (response.IsSuccessStatusCode)
//                {
//                    _logger.LogInformation("Worker created successfully");
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    _logger.LogWarning("Failed to create worker. Status: {StatusCode}, Response: {Error}",
//                        response.StatusCode, errorContent);
//                    ModelState.AddModelError("", $"Failed to create worker: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating worker");
//                ModelState.AddModelError("", "Error creating worker: " + ex.Message);
//            }

//            return View(worker);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                    return View(worker);
//                }

//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker for edit");
//                ModelState.AddModelError("", "Error loading worker for edit");
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, UserDto worker)
//        {
//            if (id != worker.Id)
//            {
//                return NotFound();
//            }

//            if (!ModelState.IsValid)
//            {
//                return View(worker);
//            }

//            try
//            {
//                using var form = new MultipartFormDataContent();

//                // Add text data
//                form.Add(new StringContent(worker.Id.ToString(), Encoding.UTF8), "Id");
//                form.Add(new StringContent(worker.Name ?? "", Encoding.UTF8), "Name");
//                form.Add(new StringContent(worker.PhoneNumber ?? "", Encoding.UTF8), "PhoneNumber");
//                form.Add(new StringContent(worker.Password ?? "", Encoding.UTF8), "Password");
//                form.Add(new StringContent(worker.UserType ?? "", Encoding.UTF8), "UserType");

//                // Add image if exists
//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    form.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }
//                else
//                {
//                    // إذا لم يتم رفع صورة جديدة، أرسل قيمة فارغة للحفاظ على الصورة القديمة
//                    form.Add(new StringContent(""), "ProfileImage");
//                }

//                var response = await _httpClient.PutAsync($"Users/{id}", form);

//                if (response.IsSuccessStatusCode)
//                {
//                    _logger.LogInformation("Worker updated successfully");
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    _logger.LogWarning("Failed to update worker. Status: {StatusCode}, Response: {Error}",
//                        response.StatusCode, errorContent);
//                    ModelState.AddModelError("", $"Failed to update worker: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating worker");
//                ModelState.AddModelError("", "Error updating worker: " + ex.Message);
//            }

//            return View(worker);
//        }

//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
//                    return View(worker);
//                }

//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker for deletion");
//                ModelState.AddModelError("", "Error loading worker for deletion");
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                var response = await _httpClient.DeleteAsync($"Users/{id}");

//                if (response.IsSuccessStatusCode)
//                {
//                    _logger.LogInformation("Worker deleted successfully");
//                    return RedirectToAction(nameof(Index));
//                }
//                else
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    _logger.LogWarning("Failed to delete worker. Status: {StatusCode}, Response: {Error}",
//                        response.StatusCode, errorContent);
//                    ModelState.AddModelError("", $"Failed to delete worker: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting worker");
//                ModelState.AddModelError("", "Error deleting worker: " + ex.Message);
//            }

//            // إعادة تحميل البيانات لعرض صفحة الحذف مرة أخرى
//            try
//            {
//                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//                if (workerResponse.IsSuccessStatusCode)
//                {
//                    var worker = await workerResponse.Content.ReadFromJsonAsync<UserDto>();
//                    return View("Delete", worker);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error reloading worker data after failed deletion");
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }

//}



//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorkerController : Controller
//    {
//        private readonly HttpClient _httpClient;
//        private readonly ILogger<WorkerController> _logger;

//        public WorkerController(HttpClient httpClient, ILogger<WorkerController> logger)
//        {
//            _httpClient = httpClient;
//            _logger = logger;
//            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
//            _httpClient.Timeout = TimeSpan.FromSeconds(30);
//        }

//        // GET: Worker
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching workers from API...");
//                var response = await _httpClient.GetAsync("Users");

//                if (!response.IsSuccessStatusCode)
//                {
//                    _logger.LogWarning("Failed to fetch workers. Status: {StatusCode}", response.StatusCode);
//                    return View(new HomeViewModel { Users = new List<UserReadDto>() });
//                }

//                var workers = await response.Content.ReadFromJsonAsync<List<UserReadDto>>();
//                _logger.LogInformation("Successfully fetched {Count} workers", workers?.Count ?? 0);

//                var model = new HomeViewModel
//                {
//                    Users = workers ?? new List<UserReadDto>()
//                };

//                return View(model);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading workers");
//                ModelState.AddModelError("", "Error loading workers: " + ex.Message);
//                return View(new HomeViewModel { Users = new List<UserReadDto>() });
//            }
//        }

//        // GET: Worker/Details/5
//        [HttpGet]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker details for ID {Id}", id);
//                ModelState.AddModelError("", "Error loading details: " + ex.Message);
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // GET: Worker/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Worker/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(UserDto worker)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(worker);
//            }

//            try
//            {
//                using var formContent = new MultipartFormDataContent();

//                // Add text data
//                formContent.Add(new StringContent(worker.Name ?? "", Encoding.UTF8), "Name");
//                formContent.Add(new StringContent(worker.PhoneNumber ?? "", Encoding.UTF8), "PhoneNumber");
//                formContent.Add(new StringContent(worker.Password ?? "", Encoding.UTF8), "Password");
//                formContent.Add(new StringContent(worker.UserType ?? "", Encoding.UTF8), "UserType");

//                // Add image if exists
//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PostAsync("Users", formContent);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                var error = await response.Content.ReadAsStringAsync();
//                ModelState.AddModelError("", "Failed to create worker: " + error);
//                return View(worker);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error creating worker");
//                ModelState.AddModelError("", "Error creating worker: " + ex.Message);
//                return View(worker);
//            }
//        }

//        // GET: Worker/Edit/5
//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker for editing, ID {Id}", id);
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Worker/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, UserReadDto worker)
//        {
//            if (id != worker.Id) return NotFound();
//            if (!ModelState.IsValid) return View(worker);

//            try
//            {
//                using var form = new MultipartFormDataContent();

//                // Add text data
//                form.Add(new StringContent(worker.Id.ToString()), nameof(worker.Id));
//                form.Add(new StringContent(worker.Name ?? ""), nameof(worker.Name));
//                form.Add(new StringContent(worker.PhoneNumber ?? ""), nameof(worker.PhoneNumber));
//                form.Add(new StringContent(worker.Password ?? ""), nameof(worker.Password));
//                form.Add(new StringContent(worker.UserType ?? ""), nameof(worker.UserType));

//                // Add image if a new one is uploaded
//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    form.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PutAsync($"Users/{id}", form);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                var error = await response.Content.ReadAsStringAsync();
//                ModelState.AddModelError(string.Empty, "Error updating worker: " + error);
//                return View(worker);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error updating worker, ID {Id}", id);
//                ModelState.AddModelError(string.Empty, "Exception: " + ex.Message);
//                return View(worker);
//            }
//        }

//        // GET: Worker/Delete/5
//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error loading worker for deletion, ID {Id}", id);
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Worker/Delete/5
//        [HttpPost, ActionName("DeleteConfirmed")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                var response = await _httpClient.DeleteAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                var error = await response.Content.ReadAsStringAsync();
//                ModelState.AddModelError(string.Empty, "Error deleting worker: " + error);

//                // Fetch the worker again to return to the view
//                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//                var worker = await workerResponse.Content.ReadFromJsonAsync<UserReadDto>();
//                return View("Delete", worker);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error deleting worker, ID {Id}", id);
//                ModelState.AddModelError(string.Empty, "Exception: " + ex.Message);

//                // Fetch the worker again to return to the view
//                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//                var worker = await workerResponse.Content.ReadFromJsonAsync<UserReadDto>();
//                return View("Delete", worker);
//            }
//        }
//    }
////}
//using MehnahFinalApi.DTO;
//using MehnahFinalMVC.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text;

//namespace MehnahFinalMVC.Controllers
//{
//    public class WorkerController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public WorkerController(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
//            _httpClient.Timeout = TimeSpan.FromSeconds(30);
//        }

//        // GET: Worker
//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync("Users");
//                if (!response.IsSuccessStatusCode)
//                {
//                    return View(new HomeViewModel { Users = new List<UserReadDto>() });
//                }

//                var workers = await response.Content.ReadFromJsonAsync<List<UserReadDto>>();
//                var model = new HomeViewModel
//                {
//                    Users = workers ?? new List<UserReadDto>()
//                };

//                return View(model);
//            }
//            catch
//            {
//                return View(new HomeViewModel { Users = new List<UserReadDto>() });
//            }
//        }

//        //--------------------------------------------------------------------------------

//        // GET: Worker/Details/5
//        [HttpGet]
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch
//            {
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        //--------------------------------------------------------------------------------

//        // GET: Worker/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Worker/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(UserReadDto worker)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(worker);
//            }

//            try
//            {
//                using var formContent = new MultipartFormDataContent();
//                formContent.Add(new StringContent(worker.Name ?? ""), "Name");
//                formContent.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
//                formContent.Add(new StringContent(worker.Password ?? ""), "Password");
//                formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PostAsync("Users", formContent);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                ModelState.AddModelError("", "Failed to create worker.");
//                return View(worker);
//            }
//            catch
//            {
//                ModelState.AddModelError("", "Error creating worker.");
//                return View(worker);
//            }
//        }

//        //--------------------------------------------------------------------------------

//        // GET: Worker/Edit/5
//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch
//            {
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Worker/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, UserReadDto worker)
//        {
//            if (id != worker.Id) return NotFound();
//            if (!ModelState.IsValid) return View(worker);

//            try
//            {
//                using var form = new MultipartFormDataContent();
//                form.Add(new StringContent(worker.Id.ToString()), "Id");
//                form.Add(new StringContent(worker.Name ?? ""), "Name");
//                form.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
//                form.Add(new StringContent(worker.Password ?? ""), "Password");
//                form.Add(new StringContent(worker.UserType ?? ""), "UserType");

//                // Assuming UserReadDto contains IFormFile property for image upload
//                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
//                {
//                    var stream = worker.ProfileImage.OpenReadStream();
//                    var fileContent = new StreamContent(stream);
//                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
//                    form.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
//                }

//                var response = await _httpClient.PutAsync($"Users/{id}", form);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                ModelState.AddModelError(string.Empty, "Error updating worker.");
//                return View(worker);
//            }
//            catch
//            {
//                ModelState.AddModelError(string.Empty, "Error updating worker.");
//                return View(worker);
//            }
//        }

//        //--------------------------------------------------------------------------------

//        // GET: Worker/Delete/5
//        [HttpGet]
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            try
//            {
//                var response = await _httpClient.GetAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var worker = await response.Content.ReadFromJsonAsync<UserReadDto>();
//                    return View(worker);
//                }
//                return NotFound();
//            }
//            catch
//            {
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // POST: Worker/Delete/5
//        [HttpPost, ActionName("DeleteConfirmed")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                var response = await _httpClient.DeleteAsync($"Users/{id}");
//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction(nameof(Index));
//                }

//                ModelState.AddModelError(string.Empty, "Error deleting worker.");
//                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//                var worker = await workerResponse.Content.ReadFromJsonAsync<UserReadDto>();
//                return View("Delete", worker);
//            }
//            catch
//            {
//                ModelState.AddModelError(string.Empty, "Error deleting worker.");
//                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
//                var worker = await workerResponse.Content.ReadFromJsonAsync<UserReadDto>();
//                return View("Delete", worker);
//            }
//        }
//    }
//}

using MehnahFinalApi.DTO;
using MehnahFinalMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace MehnahFinalMVC.Controllers
{
    public class WorkerController : Controller
    {
        private readonly HttpClient _httpClient;

        public WorkerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // تأكد أن هذا العنوان صحيح وأن الخادم قيد التشغيل
            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // GET: Worker
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("Users");
                if (!response.IsSuccessStatusCode)
                {
                    return View(new HomeViewModel { Users = new List<UserDto>() });
                }

                var workers = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                var model = new HomeViewModel
                {
                    Users = workers ?? new List<UserDto>()
                };

                return View(model);
            }
            catch (Exception)
            {
                // يمكن أن يحدث هذا عند فشل الاتصال بالخادم
                return View(new HomeViewModel { Users = new List<UserDto>() });
            }
        }

        //--------------------------------------------------------------------------------

        // GET: Worker/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var response = await _httpClient.GetAsync($"Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
                    return View(worker);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //--------------------------------------------------------------------------------

        // GET: Worker/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Worker/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto worker)
        {
            if (!ModelState.IsValid)
            {
                return View(worker);
            }

            try
            {
                using var formContent = new MultipartFormDataContent();
                formContent.Add(new StringContent(worker.Name ?? ""), "Name");
                formContent.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
                formContent.Add(new StringContent(worker.Password ?? ""), "Password");
                formContent.Add(new StringContent(worker.UserType ?? ""), "UserType");

                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
                {
                    var stream = worker.ProfileImage.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
                    formContent.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
                }

                var response = await _httpClient.PostAsync("Users", formContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "فشل إنشاء العامل.");
                return View(worker);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "حدث خطأ أثناء إنشاء العامل.");
                return View(worker);
            }
        }

        //--------------------------------------------------------------------------------

        // GET: Worker/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var response = await _httpClient.GetAsync($"Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (worker == null) return NotFound();
                    return View(worker);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Worker/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDto worker)
        {
            if (id != worker.Id) return NotFound();
            if (!ModelState.IsValid) return View(worker);

            try
            {
                using var form = new MultipartFormDataContent();
                form.Add(new StringContent(worker.Id.ToString()), "Id");
                form.Add(new StringContent(worker.Name ?? ""), "Name");
                form.Add(new StringContent(worker.PhoneNumber ?? ""), "PhoneNumber");
                form.Add(new StringContent(worker.Password ?? ""), "Password");
                form.Add(new StringContent(worker.UserType ?? ""), "UserType");

                if (worker.ProfileImage != null && worker.ProfileImage.Length > 0)
                {
                    var stream = worker.ProfileImage.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(worker.ProfileImage.ContentType);
                    form.Add(fileContent, "ProfileImage", worker.ProfileImage.FileName);
                }

                var response = await _httpClient.PutAsync($"Users/{id}", form);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تحديث العامل.");
                return View(worker);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تحديث العامل.");
                return View(worker);
            }
        }

        //--------------------------------------------------------------------------------

        // GET: Worker/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var response = await _httpClient.GetAsync($"Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var worker = await response.Content.ReadFromJsonAsync<UserDto>();
                    return View(worker);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Worker/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء حذف العامل.");
                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
                var worker = await workerResponse.Content.ReadFromJsonAsync<UserDto>();
                return View("Delete", worker);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء حذف العامل.");
                var workerResponse = await _httpClient.GetAsync($"Users/{id}");
                var worker = await workerResponse.Content.ReadFromJsonAsync<UserDto>();
                return View("Delete", worker);
            }
        }
    }
}