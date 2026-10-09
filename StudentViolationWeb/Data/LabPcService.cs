using System.Net.Http.Json;
using StudentViolationWeb.Model;

namespace StudentViolationWeb.Data
{
    public sealed class LabPcService
    {
        private readonly HttpClient _http;
        public LabPcService(HttpClient http) => _http = http;

        public Task<List<LabPcComputer>> GetComputers() => GetList<LabPcComputer>("api/labpc/computers");
        public Task<List<LabPcVoucher>> GetVouchers(string? studentNo = null)
        {
            var query = string.IsNullOrWhiteSpace(studentNo) ? "" : $"?studentNo={Uri.EscapeDataString(studentNo)}";
            return GetList<LabPcVoucher>($"api/labpc/vouchers{query}");
        }
        public Task<List<LabPcSession>> GetSessions(string status = "Active") =>
            GetList<LabPcSession>($"api/labpc/sessions?status={Uri.EscapeDataString(status)}");

        public async Task<LabPcComputer> CreateComputer(string name, string location) =>
            await Post<LabPcComputer>("api/labpc/computers", new { computerName = name, location });

        public async Task<LabPcVoucher> CreateVoucher(string studentNo, int durationMinutes) =>
            await Post<LabPcVoucher>("api/labpc/vouchers", new { studentNo, durationMinutes });

        public Task DisableComputer(int id) => PostAction($"api/labpc/computers/{id}/disable");
        public Task RevokeVoucher(long id) => PostAction($"api/labpc/vouchers/{id}/revoke");
        public Task EndSession(Guid id) => PostAction($"api/labpc/sessions/{id}/end");

        private async Task<List<T>> GetList<T>(string uri)
        {
            var response = await _http.GetAsync(uri);
            var envelope = await response.Content.ReadFromJsonAsync<LabPcApiEnvelope<List<T>>>();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException(envelope?.Message ?? "Could not load lab PC data.");
            return envelope?.Data ?? new List<T>();
        }

        private async Task<T> Post<T>(string uri, object body)
        {
            var response = await _http.PostAsJsonAsync(uri, body);
            var envelope = await response.Content.ReadFromJsonAsync<LabPcApiEnvelope<T>>();
            if (!response.IsSuccessStatusCode || envelope?.Data is null)
                throw new InvalidOperationException(envelope?.Message ?? "The lab PC request failed.");
            return envelope.Data;
        }

        private async Task PostAction(string uri)
        {
            var response = await _http.PostAsync(uri, null);
            var envelope = await response.Content.ReadFromJsonAsync<LabPcApiEnvelope<bool>>();
            if (!response.IsSuccessStatusCode || envelope?.Data != true)
                throw new InvalidOperationException(envelope?.Message ?? "The lab PC request failed.");
        }
    }
}