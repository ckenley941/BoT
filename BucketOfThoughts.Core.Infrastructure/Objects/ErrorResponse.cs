namespace BucketOfThoughts.Core.Infrastructure.Objects;

public class ErrorResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? StatusCode { get; set; }
}
