import React from 'react';

const PlantTemp = ({ templateData, onSelectTemplate }) => {
  return (
    <div className="col-lg-4" data-testid="plant-template">
      <div className="card mb-4" onClick={() => onSelectTemplate(templateData)}>
        <div className="card-body">
          <h5 className="card-title">{templateData.nameOfPlant}</h5>
          <img className="card-img-top" src={templateData.image} alt="Template" />
        </div>
      </div>
    </div>
  );
};

export default PlantTemp;
