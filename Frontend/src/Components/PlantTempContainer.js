import React from 'react';
import PlantTemp from './PlantTemplate';

const PlantTempContainer = ({ onSelectTemplate }) => {
  // Dummy template data for each instance
  const templates = [
    {
      name: "Template Name 1",
      minSoilMoisture: "25",
      wateringAmount: "100",
      image: "data:image/jpeg;base64,Placeholder"
    },
    {
      name: "Template Name 2",
      minSoilMoisture: "30",
      wateringAmount: "120",
      image: "data:image/jpeg;base64,Placeholder"
    },
    {
      name: "Template Name 3",
      minSoilMoisture: "20",
      wateringAmount: "80",
      image: "data:image/jpeg;base64,Placeholder"
    },
    {
      name: "Template Name 4",
      minSoilMoisture: "35",
      wateringAmount: "150",
      image: "data:image/jpeg;base64,Placeholder"
    },
    {
      name: "Template Name 5",
      minSoilMoisture: "28",
      wateringAmount: "110",
      image: "data:image/jpeg;base64,Placeholder"
    }
  ];

  return (
    <div className="row">
      {templates.map((template, index) => (
        <div key={index} className="col-lg-4">
          <PlantTemp templateData={template} onSelectTemplate={onSelectTemplate} />
        </div>
      ))}
    </div>
  );
};

export default PlantTempContainer;
