using System.Net.Http.Json;
using StudentViolationWeb.Model;

namespace StudentViolationWeb.Data
{
    public sealed class LabStaffAccountService
    {
        private readonly HttpClient _http;
        public LabStaffAccountService(HttpClient http) => _http = http;

        public async Task<LabStaffCreated> Create(CreateLabStaffRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/sao/labstaff", request);
            var envelope = await response.Content.ReadFromJsonAsync<LabPcApiEnvelope<LabStaffCreated>>();
            if (!response.IsSuccessStatusCode || envelope?.Data is null)
                throw new InvalidOperationException(envelope?.Message ?? "Could not create the Lab Staff account.");
            return envelope.Data;
        }
    }
}