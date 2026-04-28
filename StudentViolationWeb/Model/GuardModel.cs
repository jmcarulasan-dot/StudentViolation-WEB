namespace StudentViolationWeb.Model;

// ─── Validate Student (GET /api/guard/student/validate?studentNo=xxx) ────────
// data: { student_no, name, course, year, violation_count, warning_level, violations[] }
public class GuardStudentData
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
    public int violation_count { get; set; }
    public string? warning_level { get; set; }
    public List<GuardViolationItem>? violations { get; set; }
}

public class GuardViolationItem
{
    public DateTime date { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public string? severity { get; set; }
    public string? status { get; set; }
    public string? recorded_by { get; set; }
}

// ─── Record Violation (POST /api/guard/student/violation) ────────────────────
public class RecordViolationRequest
{
    public string StudentNo { get; set; } = string.Empty;
    public string ViolationType { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
}

public class RecordViolationData
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public int new_violation_count { get; set; }
    public string? new_warning_level { get; set; }
}

// ─── Violation Summary (GET /api/guard/violations/summary) ───────────────────
// data: { totalViolations, topViolation, startDate, endDate }
public class GuardSummaryData
{
    public int totalViolations { get; set; }
    public string? topViolation { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}

// ─── All Students (GET /api/guard/students) ──────────────────────────────────
// data: [{ student_no, name, course, year }]
public class GuardStudentListItem
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
}