import React from 'react';
import '../Styling/PlantCreatePopUp.css';

const PlantCreatePopUp = ({ handlePopUpAction, plantName, minSoilMoisture, wateringAmount, plantImage, isDefault }) => {
  return (
    <div className="plantCreate-pop-up-container">
      <div className="plantCreate-pop-up">
        <div className="container-fluid"> 
          <div className="row">
            <div className="col-lg-8">
              <p>Plant Name: {plantName}</p>
              <p>Minimum Soil Moisture: {minSoilMoisture}</p>
              <p>Watering Amount (ml): {wateringAmount}</p>
            </div>
            <div className="col-lg-4">
              <img src={plantImage} alt="Plant Preview" className="image-preview" />
            </div>
          </div>
        </div>
        <button onClick={() => handlePopUpAction('create')} className="create-button">Create New</button>
        {!isDefault && (
        <button onClick={() => handlePopUpAction('overwrite')} className="overwrite-button">Overwrite Existing</button>
        )}
        <button onClick={() => handlePopUpAction('cancel')} className="cancel-button">Cancel</button>
      </div>
    </div>
  );
};

export default PlantCreatePopUp;
