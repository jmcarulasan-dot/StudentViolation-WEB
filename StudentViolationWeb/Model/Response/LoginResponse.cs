namespace StudentViolationWeb.Model.Response
{
    public class LoginResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
