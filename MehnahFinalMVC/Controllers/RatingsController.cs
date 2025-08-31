//using Microsoft.AspNetCore.Mvc;
//using MehnahFinalMVC.Services;
//using MehnahFinalMVC.ViewModels;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace MehnahFinalMVC.Controllers
//{
//    public class RatingsController : Controller
//    {
//        private readonly RatingsApiService _api;
//        private readonly WorksApiService _worksApi;

//        public RatingsController(RatingsApiService api, WorksApiService worksApi)
//        {
//            _api = api;
//            _worksApi = worksApi;
//        }

//        // GET: Ratings
//        // يعرض كل التقييمات الموجودة في قاعدة البيانات
//        public async Task<IActionResult> Index()
//        {
//            var ratings = await _api.GetAllRatingsAsync();
//            return View(ratings);
//        }

//        // GET: Ratings/Create
//        public async Task<IActionResult> Create()
//        {
//            var works = await _worksApi.GetAllWorksAsync();
//            ViewData["WorkId"] = new SelectList(works, "Id", "Description");
//            return View();
//        }

//        // POST: Ratings/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(RatingViewModel rating)
//        {
//            if (!ModelState.IsValid)
//                return View(rating);

//            var success = await _api.AddRatingAsync(rating);
//            if (success)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError(string.Empty, "Failed to create rating");
//            return View(rating);
//        }

//        // GET: Ratings/Edit/5
//        public async Task<IActionResult> Edit(int id)
//        {
//            var rating = await _api.GetRatingByIdAsync(id);
//            if (rating == null) return NotFound();

//            var works = await _worksApi.GetAllWorksAsync();
//            ViewData["WorkId"] = new SelectList(works, "Id", "Description", rating.WorkId);
//            return View(rating);
//        }

//        // POST: Ratings/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, RatingViewModel rating)
//        {
//            if (id != rating.Id) return BadRequest();

//            if (ModelState.IsValid)
//            {
//                var success = await _api.UpdateRatingAsync(id, rating);
//                if (success) return RedirectToAction(nameof(Index));
//            }

//            var works = await _worksApi.GetAllWorksAsync();
//            ViewData["WorkId"] = new SelectList(works, "Id", "Description", rating.WorkId);
//            return View(rating);
//        }

//        // GET: Ratings/Delete/5
//        public async Task<IActionResult> Delete(int id)
//        {
//            var rating = await _api.GetRatingByIdAsync(id);
//            if (rating == null) return NotFound();
//            return View(rating);
//        }

//        // POST: Ratings/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var ratingToDelete = await _api.GetRatingByIdAsync(id);
//            if (ratingToDelete == null) return NotFound();

//            await _api.DeleteRatingAsync(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}

using MehnahFinalMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace MehnahFinalMVC.Controllers
{
    public class RatingsController : Controller
    {
        private readonly HttpClient _httpClient;

        public RatingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/"); // رابط الـ API
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            var ratings = await _httpClient.GetFromJsonAsync<List<RatingViewModel>>("Ratings");
            return View(ratings);
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rating = await _httpClient.GetFromJsonAsync<RatingViewModel>($"Ratings/{id}");
            if (rating == null) return NotFound();

            return View(rating);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rating/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RatingViewModel rating)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Ratings", rating);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create rating.");
            }
            return View(rating);
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rating = await _httpClient.GetFromJsonAsync<RatingViewModel>($"Ratings/{id}");
            if (rating == null) return NotFound();

            return View(rating);
        }

        // POST: Rating/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RatingViewModel rating)
        {
            if (id != rating.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Ratings/{id}", rating);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update rating.");
            }
            return View(rating);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rating = await _httpClient.GetFromJsonAsync<RatingViewModel>($"Ratings/{id}");
            if (rating == null) return NotFound();

            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Ratings/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to delete rating.");
            return RedirectToAction(nameof(Index));
        }
    }
}
