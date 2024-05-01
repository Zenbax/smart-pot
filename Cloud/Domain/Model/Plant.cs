namespace Domain.Model;

public class Plant
{
    public string NameOfPlant { get; set; }
    public int SoilMinimumMoisture { get; set; }
    public string ImageUrl { get; set; }
    
    public string size { get; set; }
    
    public string plantType { get; set; }
    
    public HumidityLog humidityLog { get; set; }
    
    public Plant()
    {
    }
    
    public Plant(string nameOfPlant, int soilMinimumMoisture, string imageUrl, string size, string plantType, HumidityLog humidityLog)
    {
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        ImageUrl = imageUrl;
        this.size = size;
        this.plantType = plantType;
        this.humidityLog = humidityLog;
    }
}