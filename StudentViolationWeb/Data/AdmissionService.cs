using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudentViolationWeb.Data;

public sealed class AdmissionService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _storage;

    public AdmissionService(HttpClient http, ILocalStorageService storage)
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

    public async Task<ApiResponse<List<AdmissionEnrollment>>> GetEnrollmentsAsync()
    {
        try
        {
            await AttachTokenAsync();
            return await _http.GetFromJsonAsync<ApiResponse<List<AdmissionEnrollment>>>("api/admission/enrollments")
                ?? new() { Status = 500, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public async Task<ApiResponse<AdmissionEnrollment>> CreateEnrollmentAsync(AdmissionEnrollmentForm form)
    {
        try
        {
            await AttachTokenAsync();
            using var response = await _http.PostAsJsonAsync("api/admission/enrollments", form);
            return await response.Content.ReadFromJsonAsync<ApiResponse<AdmissionEnrollment>>()
                ?? new() { Status = (int)response.StatusCode, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public async Task<ApiResponse<List<AdmissionRegistrationRequest>>> GetRegistrationRequestsAsync()
    {
        try
        {
            await AttachTokenAsync();
            return await _http.GetFromJsonAsync<ApiResponse<List<AdmissionRegistrationRequest>>>("api/admission/registration-requests")
                ?? new() { Status = 500, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public async Task<ApiStatusResponse> ApproveRegistrationAsync(int id) => await SendStatusAsync(
        () => _http.PostAsync($"api/admission/registration-requests/{id}/approve", null));

    public async Task<ApiStatusResponse> RejectRegistrationAsync(int id) => await SendStatusAsync(
        () => _http.PostAsync($"api/admission/registration-requests/{id}/reject", null));

    public async Task<AdmissionClearanceResponse> CheckClearanceAsync(string studentNo)
    {
        try
        {
            await AttachTokenAsync();
            return await _http.GetFromJsonAsync<AdmissionClearanceResponse>(
                $"api/admission/students/{Uri.EscapeDataString(studentNo)}/clearance")
                ?? new() { Status = 500, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }

    public async Task<ApiStatusResponse> SignClearanceAsync(string studentNo) => await SendStatusAsync(
        () => _http.PostAsync($"api/admission/students/{Uri.EscapeDataString(studentNo)}/clearance/sign", null));

    private async Task<ApiStatusResponse> SendStatusAsync(Func<Task<HttpResponseMessage>> send)
    {
        try
        {
            await AttachTokenAsync();
            using var response = await send();
            return await response.Content.ReadFromJsonAsync<ApiStatusResponse>()
                ?? new() { Status = (int)response.StatusCode, Message = "The server returned an empty response." };
        }
        catch (Exception ex) { return new() { Status = 500, Message = ex.Message }; }
    }
}
