// PlantTemp.js

import React from 'react';

const PlantTemp = ({ onSelectTemplate }) => {
  // Dummy template data
  const templateData = {
    name: "Template Name",
    minSoilMoisture: "25",
    wateringAmount: "100",
    image: 'https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg'
    // Add other data fields here
  };

  return (
    <div className="card mb-4" onClick={() => onSelectTemplate(templateData)}>
      <div className="card-body">
        <h5 className="card-title">{templateData.name}</h5>
        <img className="card-img-top" src={templateData.image} alt="Template" />
      </div>
    </div>
  );
};

export default PlantTemp;
