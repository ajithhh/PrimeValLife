namespace TUT.Utilities.Models;

public class ResponseItem<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public object? Errors { get; set; }
    public DateTime Timestamp { get; set; }

    // Constructor to initialize timestamp with the current date and time
    public ResponseItem()
    {
        Success = false;
        Timestamp = DateTime.UtcNow;
    }
}