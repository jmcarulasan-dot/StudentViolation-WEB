using System.Text.Json.Serialization;

namespace StudentViolationWeb.Model
{
    // ── Stored locally after login (decoded from JWT or from login response) ──
    // This is NOT a direct API response — it's what you store in state/session
    // after a successful login to know who is logged in.
    public class UserModel
    {
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}