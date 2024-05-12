﻿namespace Domain.DTOs;

public class PlantUpdateDto
{
    public string NameOfPlant  { get; set; }
    
    public int SoilMinimumMoisture { get; set; }
    
    public int WaterTankLevel { get; set; }

    public string ImageURL { get; set; }
}