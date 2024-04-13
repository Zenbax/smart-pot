namespace Socket.Models;

public class SensorData
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string SensorType { get; set; }
    public double Value { get; set; }
}