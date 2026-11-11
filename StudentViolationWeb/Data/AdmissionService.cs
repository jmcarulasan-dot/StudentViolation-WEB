using Blazored.LocalStorage;
using StudentViolationWeb.Model;
using StudentViolationWeb.Model.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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
            using var response = await _http.GetAsync("api/admission/registration-requests");
            if (!response.IsSuccessStatusCode)
                return new() { Status = (int)response.StatusCode, Message = $"Could not load registration requests (HTTP {(int)response.StatusCode}). Check that the API and database are running." };

            try
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<List<AdmissionRegistrationRequest>>>()
                    ?? new() { Status = 500, Message = "The API returned an empty response for registration requests." };
            }
            catch (JsonException)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "unknown";
                return new() { Status = 502, Message = $"The API returned an invalid response for registration requests (content type: {contentType}). Check the API/database logs." };
            }
        }
        catch (HttpRequestException)
        {
            return new() { Status = 503, Message = "Could not connect to the API. Make sure the SVS API is running." };
        }
        catch (Exception)
        {
            return new() { Status = 500, Message = "Registration requests could not be loaded. Check the API and database logs." };
        }
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
