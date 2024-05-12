namespace Socket.Models;

public class SensorData
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string MachineID { get; set; }
    public double WaterTankLevel { get; set; }
    public double MeasuredSoilMoisture { get; set; }
    public double AmountOfWatering { get; set; }
    public string PotId { get; set; }
    public string PlantId { get; set; }
}