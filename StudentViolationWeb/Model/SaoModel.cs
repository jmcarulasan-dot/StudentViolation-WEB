namespace StudentViolationWeb.Model;

// ─── All Violations (GET /api/sao/violations) ─────────────────────────────────
// data: [{ id, student_no, type, details, severity, date, recorded_by, status }]
public class SaoViolationItem
{
    public int id { get; set; }
    public string? student_no { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public string? severity { get; set; }
    public DateTime date { get; set; }
    public string? recorded_by { get; set; }
    public string? status { get; set; }
    // For appeals view
    public string? appeal_text { get; set; }
    public string? appeal_status { get; set; }
}

// ─── Violations Summary (GET /api/sao/violations/summary) ────────────────────
// data: { total, pending, approved, rejected, by_severity[], by_type[] }
public class SaoSummaryData
{
    public int total { get; set; }
    public int pending { get; set; }
    public int approved { get; set; }
    public int rejected { get; set; }
    public List<SaoSeverityCount>? by_severity { get; set; }
    public List<SaoTypeCount>? by_type { get; set; }
}

public class SaoSeverityCount
{
    public string? severity { get; set; }
    public int count { get; set; }
}

public class SaoTypeCount
{
    public string? type { get; set; }
    public int count { get; set; }
}

// ─── Student Report (GET /api/sao/students/{studentNo}/report) ───────────────
public class SaoStudentReport
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
    public int violation_count { get; set; }
    public string? warning_level { get; set; }
    public string? recommended_action { get; set; }
    public List<SaoViolationItem>? violations { get; set; }
}

// ─── All Users (GET /api/sao/users) ──────────────────────────────────────────
// data: [{ id, username, name, email, role, gender, course, year,
//          contact_number, registration_date }]
public class SaoUserItem
{
    public int id { get; set; }
    public string? username { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? role { get; set; }
    public string? gender { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
    public string? contact_number { get; set; }
    public DateTime? registration_date { get; set; }
}

// ─── Single User (GET /api/sao/users/{id}) ───────────────────────────────────
public class SaoUserDetail
{
    public int id { get; set; }
    public string? username { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? email { get; set; }
    public string? role { get; set; }
    public string? gender { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
    public string? address { get; set; }
    public string? contact_number { get; set; }
}

// ─── Update User Request (PUT /api/sao/users/{id}) ───────────────────────────
public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
}

// ─── Pending Dismissals (GET /api/sao/students/pending-dismissal) ─────────────
public class PendingDismissalItem
{
    public int StudentID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StudentNo { get; set; }
    public string? Course { get; set; }
    public string? Year { get; set; }
    public string? Status { get; set; }
}