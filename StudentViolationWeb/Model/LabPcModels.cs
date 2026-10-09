namespace StudentViolationWeb.Model
{
    public sealed class LabPcComputer
    {
        public int ComputerId { get; set; }
        public string ComputerName { get; set; } = "";
        public string Location { get; set; } = "";
        public bool Enabled { get; set; }
        public DateTime? LastHeartbeatUtc { get; set; }
        public bool IsOnline { get; set; }
        public string? EnrollmentCode { get; set; }
    }

    public sealed class LabPcVoucher
    {
        public long VoucherId { get; set; }
        public string StudentNo { get; set; } = "";
        public string? StudentName { get; set; }
        public string Status { get; set; } = "";
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public int DurationMinutes { get; set; }
        public string? VoucherCode { get; set; }
    }

    public sealed class LabPcSession
    {
        public Guid SessionId { get; set; }
        public int ComputerId { get; set; }
        public string ComputerName { get; set; } = "";
        public string Location { get; set; } = "";
        public string StudentNo { get; set; } = "";
        public string StudentName { get; set; } = "";
        public DateTime StartedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? EndedAtUtc { get; set; }
        public string Status { get; set; } = "";
    }

    public sealed class LabPcApiEnvelope<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}