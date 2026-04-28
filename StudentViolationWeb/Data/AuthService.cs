using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    // POST /api/auth/login
    public async Task<LoginResponse> LoginAsync(LoginModel login)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", login);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result?.Token != null)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                await _localStorage.SetItemAsync("authRole", result.Role ?? "");
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Token);
            }

            return result ?? new LoginResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new LoginResponse { Status = 500, Message = ex.Message };
        }
    }

    // POST /api/auth/register
    // RegisterModel.DateOfBirth is DateTime? (for MudDatePicker).
    // The backend expects DateOfBirth as a plain string "YYYY-MM-DD".
    // We use an anonymous object so no extra class is needed.
    public async Task<ApiStatusResponse> RegisterAsync(RegisterModel register)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", new
            {
                register.Username,
                register.Password,
                register.Email,
                register.FirstName,
                register.LastName,
                DateOfBirth = register.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
                register.Gender,
                register.Address,
                Number = register.Number,
                register.Role,
                register.StudentNo,
                register.Course,
                register.Year
            });

            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // Restore token from local storage on page load
    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("authRole");
        _http.DefaultRequestHeaders.Authorization = null;
    }
}