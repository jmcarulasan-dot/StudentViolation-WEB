using Blazored.LocalStorage;
using Microsoft.JSInterop;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace StudentViolationWeb.Data;

public class StudentService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public StudentService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    private async Task AttachTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
    }

    // GET /api/student/violations
    public async Task<ApiResponse<StudentDashboardData>> GetViolationsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<StudentDashboardData>>("api/student/violations");
            return result ?? new ApiResponse<StudentDashboardData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<StudentDashboardData> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/student/profile
    public async Task<ApiResponse<StudentProfileData>> GetProfileAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<StudentProfileData>>("api/student/profile");
            return result ?? new ApiResponse<StudentProfileData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<StudentProfileData> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/student/qrcode
    public async Task<ApiResponse<StudentQrData>> GetQrCodeAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<StudentQrData>>("api/student/qrcode");
            return result ?? new ApiResponse<StudentQrData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<StudentQrData> { Status = 500, Message = ex.Message };
        }
    }

    // POST /api/student/violations/{id}/appeal
    public async Task<ApiStatusResponse> SubmitAppealAsync(int violationId, SubmitAppealRequest request)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PostAsJsonAsync(
                $"api/student/violations/{violationId}/appeal", request);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }
}