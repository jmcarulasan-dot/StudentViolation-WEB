namespace StudentViolationWeb.Model;

public sealed class AdmissionEnrollment
{
    public string StudentNo { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Course { get; set; } = "";
    public string Year { get; set; } = "";
}

public sealed class AdmissionRegistrationRequest
{
    public int RequestId { get; set; }
    public string StudentNo { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Username { get; set; } = "";
    public string RequestStatus { get; set; } = "";
}

public sealed class AdmissionClearanceResponse
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public bool CanSign { get; set; }
}

public sealed class AdmissionEnrollmentForm
{
    public string StudentNo { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Course { get; set; } = "";
    public string Year { get; set; } = "";
}
