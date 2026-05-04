namespace StudentViolationWeb.Model.Response;

// Wraps endpoints that return: { status, message, data: T }
public class ApiResponse<T>
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public string? Token { get; set; }
}

// Wraps endpoints that return: { status, message, total, data: List<T> }
// Used by: GET /api/guard/students, GET /api/sao/violations, GET /api/sao/users, etc.
public class ApiListResponse<T>
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public int Total { get; set; }
    public List<T>? Data { get; set; }
}

public class ApiStatusResponse
{
    public int Status { get; set; }
    public string? Message { get; set; }
}
