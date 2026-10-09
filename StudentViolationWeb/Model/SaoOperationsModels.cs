namespace StudentViolationWeb.Model;

public sealed class SaoAuditEntry
{
    public long AuditID { get; set; }
    public string Action { get; set; } = "";
    public string EntityType { get; set; } = "";
    public string EntityID { get; set; } = "";
    public string? StudentNo { get; set; }
    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }
    public string? Remarks { get; set; }
    public string ActorUsername { get; set; } = "";
    public string ActorRole { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }
}

public sealed class SaoViolationLevelResponse
{
    public int Status { get; set; }
    public string? StudentNo { get; set; }
    public int ActiveViolations { get; set; }
    public string? ViolationLevel { get; set; }
    public string? RecommendedAction { get; set; }
}
