namespace StudentViolationWeb.Model;

// ─── Violations / Dashboard ────────────────────────────────────────────────
// Matches: GET /api/student/violations  →  data: { student_no, name, total_violations,
//          pending, approved, rejected, warning_level, recommended_action, violations[] }

public class StudentDashboardData
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public int total_violations { get; set; }
    public int pending { get; set; }
    public int approved { get; set; }
    public int rejected { get; set; }
    public string? warning_level { get; set; }
    public string? recommended_action { get; set; }
    public List<ViolationItem>? violations { get; set; }
}

public class ViolationItem
{
    public int id { get; set; }
    public string? type { get; set; }
    public string? details { get; set; }
    public string? severity { get; set; }
    public DateTime date { get; set; }
    public string? status { get; set; }
    public string? recorded_by { get; set; }
    public string? appeal_text { get; set; }
    public string? appeal_status { get; set; }
    public string? appeal_remarks { get; set; }
}

// ─── Profile ───────────────────────────────────────────────────────────────
// Matches: GET /api/student/profile  →  data: { student_no, name, email, gender,
//          course, year, contact_number, address, status, total_violations, warning_level }

public class StudentProfileData
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? gender { get; set; }
    public string? course { get; set; }
    public string? year { get; set; }
    public string? contact_number { get; set; }
    public string? address { get; set; }
    public string? status { get; set; }
    public int total_violations { get; set; }
    public string? warning_level { get; set; }
}

// ─── QR Code ──────────────────────────────────────────────────────────────
// Matches: GET /api/student/qrcode  →  data: { student_no, name, qr_code }

public class StudentQrData
{
    public string? student_no { get; set; }
    public string? name { get; set; }
    public string? qr_code { get; set; }  // base64 PNG — prefix with data:image/png;base64,
}

// ─── Appeal ───────────────────────────────────────────────────────────────
public class SubmitAppealRequest
{
    public string AppealText { get; set; } = string.Empty;
}