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

    // POST /api/auth/login. Do not persist a token before MFA verification.
    public async Task<LoginResponse> LoginAsync(LoginModel login)
    {
        try
        {
            await LogoutAsync();
            var response = await _http.PostAsJsonAsync("api/auth/login", login);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result ?? new LoginResponse { Status = 500, Message = "Empty response" };
        }
        catch
        {
            return new LoginResponse { Status = 500, Message = "Could not connect to the SVS server." };
        }
    }

    public async Task<LoginResponse> VerifyAuthenticatorAsync(AuthenticatorVerifyRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/mfa/verify", request);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(result?.Data?.Token))
            {
                await _localStorage.SetItemAsync("authToken", result.Data.Token);
                await _localStorage.SetItemAsync("authRole", result.Data.Role ?? "");
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Data.Token);
            }
            return result ?? new LoginResponse { Status = 500, Message = "Empty response" };
        }
        catch
        {
            return new LoginResponse { Status = 500, Message = "Could not connect to the SVS server." };
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