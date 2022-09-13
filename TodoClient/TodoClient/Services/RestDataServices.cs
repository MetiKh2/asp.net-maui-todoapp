using System.Diagnostics;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using TodoClient.Models;

namespace TodoClient.Services
{
    public class RestDataServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        public RestDataServices()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5069" : "https://localhost:5069";
            _url = $"{_baseAddress}/api";

        }
        public async Task<List<Todo>> GetAllTodos()
        {
            List<Todo> todos = new List<Todo>();

            //if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            //{
            //    Debug.WriteLine("---> No internet access...");
            //    return todos;
            //}
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/todos");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                        return Enumerable.Empty<Todo>().ToList();
                    todos = await response.Content.ReadFromJsonAsync<List<Todo>>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }
            return todos;
        }
        public async Task<bool> CreateTodo(Todo todo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_url}/todos",todo);
                if (response.IsSuccessStatusCode) return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
                return false;
            }
            return false;
        }
        public async Task<bool> UpdateTodo(Todo todo)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_url}/todos/"+todo.Id,todo);
                if (response.IsSuccessStatusCode) return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
                return false;
            }
            return false;
        }
        public async Task<bool> DeleteTodo(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_url}/todos/"+id);
                if (response.IsSuccessStatusCode) return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
                return false;
            }
            return false;
        }
    }
}
