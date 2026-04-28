namespace StudentViolationWeb.Model;

public class RegisterModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }   // DateTime? for MudDatePicker
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? StudentNo { get; set; }
    public string? Course { get; set; }
    public string? Year { get; set; }
}