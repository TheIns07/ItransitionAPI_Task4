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
            var response = await _httpClient.GetAsync($"api/users/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/users");
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

        // Add a new user
        public async Task<UserDTO> AddUserAsync(UserDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        // Update a user
        public async Task UpdateUserAsync(int id, UserDTO user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", user);
            response.EnsureSuccessStatusCode();
        }

        // Delete a user
        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Update user status to Blocked
        public async Task UpdateUserStatusBlockedAsync(int id)
        {
            var response = await _httpClient.PatchAsync($"api/users/{id}/statusblocked", null);
            response.EnsureSuccessStatusCode();
        }

        // Update user status to Active
        public async Task UpdateUserStatusUnBlockedAsync(int id)
        {
            var response = await _httpClient.PatchAsync($"api/users/{id}/statusunblocked", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
