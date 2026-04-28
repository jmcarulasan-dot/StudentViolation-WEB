using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public class GuardService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public GuardService(HttpClient http, ILocalStorageService localStorage)
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

    // GET /api/guard/student/validate?studentNo=xxx
    public async Task<ApiResponse<GuardStudentData>> ValidateStudentAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<GuardStudentData>>(
                $"api/guard/student/validate?studentNo={Uri.EscapeDataString(studentNo)}");
            return result ?? new ApiResponse<GuardStudentData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GuardStudentData> { Status = 500, Message = ex.Message };
        }
    }

    // POST /api/guard/student/violation
    public async Task<ApiResponse<RecordViolationData>> RecordViolationAsync(RecordViolationRequest request)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PostAsJsonAsync("api/guard/student/violation", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<RecordViolationData>>();
            return result ?? new ApiResponse<RecordViolationData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<RecordViolationData> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guard/violations/summary?StartDate=xxx&EndDate=xxx
    public async Task<ApiResponse<GuardSummaryData>> GetSummaryAsync(DateTime start, DateTime end)
    {
        try
        {
            await AttachTokenAsync();
            string url = $"api/guard/violations/summary?StartDate={start:yyyy-MM-dd}&EndDate={end:yyyy-MM-dd}";
            var result = await _http.GetFromJsonAsync<ApiResponse<GuardSummaryData>>(url);
            return result ?? new ApiResponse<GuardSummaryData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GuardSummaryData> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guard/students
    public async Task<ApiListResponse<GuardStudentListItem>> GetAllStudentsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiListResponse<GuardStudentListItem>>("api/guard/students");
            return result ?? new ApiListResponse<GuardStudentListItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiListResponse<GuardStudentListItem> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guard/students/exist?studentNo=xxx
    public async Task<ApiResponse<GuardStudentListItem>> CheckStudentExistsAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<GuardStudentListItem>>(
                $"api/guard/students/exist?studentNo={Uri.EscapeDataString(studentNo)}");
            return result ?? new ApiResponse<GuardStudentListItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GuardStudentListItem> { Status = 500, Message = ex.Message };
        }
    }
}