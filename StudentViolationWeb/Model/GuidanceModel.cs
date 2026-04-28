namespace StudentViolationWeb.Model;

// ─── All Students (GET /api/guidance/students) ───────────────────────────────
// data: [{ student_no, name, email, contact_number, gender, violation_count,
//          warning_level, recommended_action }]
public class GuidanceStudentItem
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? contact_number { get; set; }
    public string? gender { get; set; }
    public int violation_count { get; set; }
    public string? warning_level { get; set; }
    public string? recommended_action { get; set; }
}

// ─── Student Report (GET /api/guidance/students/{studentNo}/report) ──────────
// data: { student_no, name, email, contact_number, gender, address,
//         date_of_birth, course, year, status, violation_count,
//         warning_level, recommended_action, violations[] }
public class GuidanceStudentReport
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? contact_number { get; set; }
    public string? gender { get; set; }
    public string? address { get; set; }
    public DateTime? date_of_birth { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
    public string? status { get; set; }
    public int violation_count { get; set; }
    public string? warning_level { get; set; }
    public string? recommended_action { get; set; }
    public List<GuidanceViolationItem>? violations { get; set; }
}

public class GuidanceViolationItem
{
    public int id { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public string? severity { get; set; }
    public DateTime date { get; set; }
    public string? status { get; set; }
    public string? recorded_by { get; set; }
    // For appeals
    public string? appeal_text { get; set; }
    public string? appeal_status { get; set; }
    public string? appeal_remarks { get; set; }
}

// ─── Violations By Status (GET /api/guidance/violations/by-status) ───────────
public class ViolationsByStatusGroup
{
    public string? status { get; set; }
    public int count { get; set; }
    public List<GuidanceViolationListItem>? violations { get; set; }
}

public class GuidanceViolationListItem
{
    public int id { get; set; }
    public string? student_no { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public string? severity { get; set; }
    public DateTime date { get; set; }
    public string? recorded_by { get; set; }
}

// ─── Violations By Severity (GET /api/guidance/violations/by-severity) ───────
public class ViolationsBySeverityGroup
{
    public string? severity { get; set; }
    public int count { get; set; }
    public List<GuidanceSeverityViolationItem>? violations { get; set; }
}

public class GuidanceSeverityViolationItem
{
    public int id { get; set; }
    public string? student_no { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public DateTime date { get; set; }
    public string? status { get; set; }
    public string? recorded_by { get; set; }
}

// ─── Appeals (GET /api/guidance/violations/appeals) ──────────────────────────
public class GuidanceAppealItem
{
    public int id { get; set; }
    public string? student_no { get; set; }
    public string? type { get; set; }
    public string? severity { get; set; }
    public DateTime date { get; set; }
    public string? status { get; set; }
    public string? appeal_text { get; set; }
    public string? appeal_status { get; set; }
    public string? recorded_by { get; set; }
}

// ─── Appeal Review Request ────────────────────────────────────────────────────
public class AppealReviewRequest
{
    public string AppealStatus { get; set; } = string.Empty;
    public string? AppealRemarks { get; set; }
}