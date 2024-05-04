namespace Domain.DTOs;

public class HumidityLogDto
{
    public DateTime TimeStamp { get; set; }
    public int HumidityPercent { get; set; }
    public int MoisturePercent { get; set; }
    public int WateringAmountML { get; set; }
}