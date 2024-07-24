using System.Net.Http.Json;
using System.Text.Json;
using Frontend.Client.DTO;

namespace Frontend.Client.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/user/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/user");
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Request error: {ex.Message}");
                return new List<UserDTO>();
            }
            catch (NotSupportedException ex)
            {
                Console.Error.WriteLine($"The content type is not supported: {ex.Message}");
                return new List<UserDTO>();
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"Invalid JSON: {ex.Message}");
                return new List<UserDTO>();
            }
        }

        public async Task<UserDTO> AddUserAsync(UserDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        public async Task UpdateUserAsync(List<int> ids, UserDTO user)
        {
            foreach (var id in ids)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", user);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteUserAsync(List<int> idUsers)
        {
            foreach(var id in idUsers)
            {
                var response = await _httpClient.DeleteAsync($"api/user/{id}");
                response.EnsureSuccessStatusCode();
            } 
        }

        // Update user status to Blocked
        public async Task UpdateUserStatusBlockedAsync(List<int> idUsers)
        {
            foreach (var id in idUsers)
            {
                var response = await _httpClient.PatchAsync($"api/user/{id}/statusblocked", null);
                response.EnsureSuccessStatusCode();
            }
        }

        // Update user status to Active
        public async Task UpdateUserStatusUnBlockedAsync(List<int> idUsers)
        {
            foreach (var id in idUsers)
            {
                var response = await _httpClient.PatchAsync($"api/user/{id}/statusunblocked", null);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
