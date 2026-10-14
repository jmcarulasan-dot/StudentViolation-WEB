namespace StudentViolationWeb.Model.Response
{
    public sealed class LoginResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public LoginFlow? Data { get; set; }
    }

    public sealed class LoginFlow
    {
        public string? NextStep { get; set; }
        public string? ChallengeId { get; set; }
        public string? AuthenticatorUri { get; set; }
        public string? QrCodeDataUri { get; set; }
        public string? ManualEntryKey { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public List<string>? RecoveryCodes { get; set; }
    }

    public sealed class AuthenticatorVerifyRequest
    {
        public string ChallengeId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}