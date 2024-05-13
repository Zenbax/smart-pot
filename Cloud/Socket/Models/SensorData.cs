public class SensorData
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string MachineID { get; set; }
    public double WaterTankLevel { get; set; }
    public int MeasuredSoilMoisture { get; set; }
    public int AmountOfWatering { get; set; }
    public string PotId { get; set; } // Id for den pot, der har denne MachineID
    public string PlantId { get; set; } // Id for planten i denne pot
}