namespace Microwave.Domain.Entities;

public class ExceptionLog
{
    public Guid Id { get; set; }
    public string? Exception { get; set; }
    public string? InnerException { get; set; }
    public string? Stacktrace { get; set; }
}