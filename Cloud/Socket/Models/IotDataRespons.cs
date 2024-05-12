namespace Socket.Models;

public class IotDataRespons
{
    public string PotId { get; set; }
    public string PlantId { get; set; }
    public string MachineId { get; set; }
    public int SoilMinimumMoisture { get; set; }
    public double AmountOfWateringToBeGiven { get; set; }
}