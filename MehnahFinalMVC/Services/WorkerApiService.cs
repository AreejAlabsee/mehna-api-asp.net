using System.Net.Http.Json;
using MehnahFinalMVC.ViewModels;

namespace MehnahFinalMVC.Services
{
    public class WorkerApiService
    {
        private readonly HttpClient _httpClient;

        public WorkerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://192.168.1.104:7232/api/");
        }

        public async Task<List<WorkerViewModel>> GetAllWorkersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<WorkerViewModel>>("Users")
                   ?? new List<WorkerViewModel>();
        }

        public async Task<WorkerViewModel?> GetWorkerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<WorkerViewModel>($"Users/{id}");
        }

        public async Task<bool> AddWorkerAsync(WorkerViewModel worker)
        {
            var response = await _httpClient.PostAsJsonAsync("Users", worker);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateWorkerAsync(int id, WorkerViewModel worker)
        {
            var response = await _httpClient.PutAsJsonAsync($"Users/{id}", worker);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWorkerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
