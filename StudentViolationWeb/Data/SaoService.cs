using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public class SaoService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public SaoService(HttpClient http, ILocalStorageService localStorage)
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

    // GET /api/sao/violations
    public async Task<ApiListResponse<SaoViolationItem>> GetAllViolationsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiListResponse<SaoViolationItem>>("api/sao/violations");
            return result ?? new ApiListResponse<SaoViolationItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiListResponse<SaoViolationItem> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/violations/summary
    public async Task<ApiResponse<SaoSummaryData>> GetSummaryAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<SaoSummaryData>>("api/sao/violations/summary");
            return result ?? new ApiResponse<SaoSummaryData> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<SaoSummaryData> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/violations/appeals
    public async Task<ApiListResponse<SaoViolationItem>> GetPendingAppealsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiListResponse<SaoViolationItem>>("api/sao/violations/appeals");
            return result ?? new ApiListResponse<SaoViolationItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiListResponse<SaoViolationItem> { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/violations/{id}/approve
    public async Task<ApiStatusResponse> ApproveViolationAsync(int id)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync($"api/sao/violations/{id}/approve", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/violations/{id}/reject
    public async Task<ApiStatusResponse> RejectViolationAsync(int id)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync($"api/sao/violations/{id}/reject", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // DELETE /api/sao/violations/{id}
    public async Task<ApiStatusResponse> DeleteViolationAsync(int id)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.DeleteAsync($"api/sao/violations/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/violations/{id}/appeal/review
    public async Task<ApiStatusResponse> ReviewAppealAsync(int id, AppealReviewRequest request)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsJsonAsync($"api/sao/violations/{id}/appeal/review", request);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/students/{studentNo}/report
    public async Task<ApiResponse<SaoStudentReport>> GetStudentReportAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<SaoStudentReport>>(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/report");
            return result ?? new ApiResponse<SaoStudentReport> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<SaoStudentReport> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/users
    public async Task<ApiListResponse<SaoUserItem>> GetAllUsersAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiListResponse<SaoUserItem>>("api/sao/users");
            return result ?? new ApiListResponse<SaoUserItem> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiListResponse<SaoUserItem> { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/users/{id}
    public async Task<ApiResponse<SaoUserDetail>> GetUserByIdAsync(int id)
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<SaoUserDetail>>($"api/sao/users/{id}");
            return result ?? new ApiResponse<SaoUserDetail> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<SaoUserDetail> { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/users/{id}
    public async Task<ApiStatusResponse> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsJsonAsync($"api/sao/users/{id}", request);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // DELETE /api/sao/users/{id}
    public async Task<ApiStatusResponse> DeleteUserAsync(int id)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.DeleteAsync($"api/sao/users/{id}");
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/students/pending-dismissal
    public async Task<ApiResponse<List<PendingDismissalItem>>> GetPendingDismissalsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<List<PendingDismissalItem>>>(
                "api/sao/students/pending-dismissal");
            return result ?? new ApiResponse<List<PendingDismissalItem>> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<PendingDismissalItem>> { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/students/{studentNo}/dismiss
    public async Task<ApiStatusResponse> DismissStudentAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/dismiss", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // PUT /api/sao/students/{studentNo}/cancel-dismiss
    public async Task<ApiStatusResponse> CancelDismissalAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var response = await _http.PutAsync(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/cancel-dismiss", null);
            var result = await response.Content.ReadFromJsonAsync<ApiStatusResponse>();
            return result ?? new ApiStatusResponse { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiStatusResponse { Status = 500, Message = ex.Message };
        }
    }

    // GET /api/sao/students/dismissed
    public async Task<ApiResponse<List<PendingDismissalItem>>> GetDismissedStudentsAsync()
    {
        try
        {
            await AttachTokenAsync();
            var result = await _http.GetFromJsonAsync<ApiResponse<List<PendingDismissalItem>>>(
                "api/sao/students/dismissed");
            return result ?? new ApiResponse<List<PendingDismissalItem>> { Status = 500, Message = "Empty response" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<PendingDismissalItem>> { Status = 500, Message = ex.Message };
        }
    }
}