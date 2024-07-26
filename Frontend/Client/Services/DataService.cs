using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Frontend.Client.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Frontend.Client.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public DataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
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

        public async Task<UserDTO> AddUserAsync(RegisterDTO user)
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

        public async Task UpdateUserStatusBlockedAsync(List<int> idUsers)
        {
            foreach (var id in idUsers)
            {
                var response = await _httpClient.PatchAsync($"api/user/{id}/statusblocked", null);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task UpdateUserStatusUnBlockedAsync(List<int> idUsers)
        {
            foreach (var id in idUsers)
            {
                var response = await _httpClient.PatchAsync($"api/user/{id}/statusunblocked", null);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var loginDto = new LoginModel { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/user/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginTokenResponse>();
                await _localStorageService.SetItemAsync("authToken", result.Token);
                return result.Token;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }

        public async Task<bool> IsUserAuthenticatedAndNotBlockedAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var status = jwt.Claims.FirstOrDefault(c => c.Type == "status")?.Value;

            return status != "Blocked";
        }


    }
}
