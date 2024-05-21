import React from 'react';
import '../Styling/PlantCreatePopUp.css';

const PlantCreatePopUp = ({ handlePopUpAction, plantName, minSoilMoisture, wateringAmount, plantImage}) => {
  return (
    <div className="plantCreate-pop-up-container">
      <div className="plantCreate-pop-up">
        <div className="container-fluid"> 
        <h2>You are about to delete: {plantName}</h2>
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
        <button onClick={() => handlePopUpAction('delete')} className="btn btn-danger">Delete</button>
        <button onClick={() => handlePopUpAction('cancel')} className="btn btn-secondary">Cancel</button>
      </div>
    </div>
  );
};

export default PlantCreatePopUp;