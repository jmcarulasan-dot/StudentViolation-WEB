using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public sealed class SaoOperationsService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _storage;

    public SaoOperationsService(HttpClient http, ILocalStorageService storage)
    {
        _http = http;
        _storage = storage;
    }

    private async Task AttachTokenAsync()
    {
        var token = await _storage.GetItemAsync<string>("authToken");
        _http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token)
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<ApiResponse<SaoStudentReport>> GetStudentReportAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            return await _http.GetFromJsonAsync<ApiResponse<SaoStudentReport>>(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/report")
                ?? new() { Status = 500, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public async Task<SaoViolationLevelResponse> GetViolationLevelAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            return await _http.GetFromJsonAsync<SaoViolationLevelResponse>(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/violation-level")
                ?? new() { Status = 500 };
        }
        catch { return new() { Status = 500 }; }
    }

    public async Task<ApiListResponse<SaoAuditEntry>> GetAuditAsync(string? studentNo)
    {
        try
        {
            await AttachTokenAsync();
            var query = string.IsNullOrWhiteSpace(studentNo) ? "?take=100" : $"?take=100&studentNo={Uri.EscapeDataString(studentNo)}";
            return await _http.GetFromJsonAsync<ApiListResponse<SaoAuditEntry>>($"api/sao/audit{query}")
                ?? new() { Status = 500, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public Task<ApiStatusResponse> WarnStudentAsync(string studentNo) => SendStudentActionAsync(studentNo, "warn");
    public Task<ApiStatusResponse> CounselStudentAsync(string studentNo) => SendStudentActionAsync(studentNo, "counsel");
    public Task<ApiStatusResponse> RecommendDismissalAsync(string studentNo) => SendStudentActionAsync(studentNo, "recommend-dismiss");

    private async Task<ApiStatusResponse> SendStudentActionAsync(string studentNo, string action)
    {
        try
        {
            await AttachTokenAsync();
            using var response = await _http.PutAsync(
                $"api/sao/students/{Uri.EscapeDataString(studentNo)}/{action}", null);
            return await response.Content.ReadFromJsonAsync<ApiStatusResponse>()
                ?? new() { Status = (int)response.StatusCode, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }
}
