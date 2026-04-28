using System.Text.Json.Serialization;

namespace StudentViolationWeb.Model
{
    // ── Shared violation item used inside reports (Guidance + SAO student reports) ──
    public class ReportViolationItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("details")]
        public string? Details { get; set; }

        [JsonPropertyName("severity")]
        public string? Severity { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("recorded_by")]
        public string? RecordedBy { get; set; }
    }
}