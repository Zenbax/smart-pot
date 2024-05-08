import React from 'react';
import '../Styling/PlantPopUp.css';

const PlantPopUp = ({ handlePopUpAction, plantName, minSoilMoisture, wateringAmount, imagePreview }) => {
  return (
    <div className="plant-pop-up-container">
      <div className="plant-pop-up">
        <div className="container-fluid"> 
          <div className="row">
            <div className="col-lg-8">
              <p>Plant Name: {plantName}</p>
              <p>Minimum Soil Moisture: {minSoilMoisture}</p>
              <p>Watering Amount (ml): {wateringAmount}</p>
            </div>
            <div className="col-lg-4">
              <img src={imagePreview} alt="Plant Preview" className="image-preview" />
            </div>
          </div>
        </div>
        <button onClick={() => handlePopUpAction('create')} className="create-button">Create New</button>
        <button onClick={() => handlePopUpAction('overwrite')} className="overwrite-button">Overwrite Existing</button>
        <button onClick={() => handlePopUpAction('cancel')} className="cancel-button">Cancel</button>
      </div>
    </div>
  );
};

export default PlantPopUp;
