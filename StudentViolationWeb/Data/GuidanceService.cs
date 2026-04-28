using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public class GuidanceService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public GuidanceService(HttpClient http, ILocalStorageService localStorage)
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

    // GET /api/guidance/students
    public async Task<ApiListResponse<GuidanceStudentItem>> GetAllStudentsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiListResponse<GuidanceStudentItem>>("api/guidance/students");
            return result ?? new ApiListResponse<GuidanceStudentItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiListResponse<GuidanceStudentItem> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guidance/students/{studentNo}/report
    public async Task<ApiResponse<GuidanceStudentReport>> GetStudentReportAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<GuidanceStudentReport>>(
                $"api/guidance/students/{Uri.EscapeDataString(studentNo)}/report");
            return result ?? new ApiResponse<GuidanceStudentReport> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GuidanceStudentReport> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guidance/violations/by-status
    public async Task<ApiResponse<List<ViolationsByStatusGroup>>> GetViolationsByStatusAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<List<ViolationsByStatusGroup>>>(
                "api/guidance/violations/by-status");
            return result ?? new ApiResponse<List<ViolationsByStatusGroup>> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ViolationsByStatusGroup>> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guidance/violations/by-severity
    public async Task<ApiResponse<List<ViolationsBySeverityGroup>>> GetViolationsBySeverityAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<List<ViolationsBySeverityGroup>>>(
                "api/guidance/violations/by-severity");
            return result ?? new ApiResponse<List<ViolationsBySeverityGroup>> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ViolationsBySeverityGroup>> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/guidance/violations/appeals
    public async Task<ApiResponse<List<GuidanceAppealItem>>> GetPendingAppealsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<List<GuidanceAppealItem>>>(
                "api/guidance/violations/appeals");
            return result ?? new ApiResponse<List<GuidanceAppealItem>> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<GuidanceAppealItem>> { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/guidance/students/{studentNo}/warn
    public async Task<ApiStatusResponse> WarnStudentAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync(
                $"api/guidance/students/{Uri.EscapeDataString(studentNo)}/warn", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/guidance/students/{studentNo}/recommend-dismiss
    public async Task<ApiStatusResponse> RecommendDismissalAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync(
                $"api/guidance/students/{Uri.EscapeDataString(studentNo)}/recommend-dismiss", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/guidance/violations/{id}/appeal/review
    public async Task<ApiStatusResponse> ReviewAppealAsync(int violationId, AppealReviewRequest request)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsJsonAsync(
                $"api/guidance/violations/{violationId}/appeal/review", request);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }
}