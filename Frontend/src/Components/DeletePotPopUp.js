import React from 'react';
import '../Styling/PlantCreatePopUp.css';

const PlantCreatePopUp = ({ handlePopUpAction, potName, plantImage}) => {
  return (
    <div className="plantCreate-pop-up-container">
      <div className="plantCreate-pop-up">
        <div className="container-fluid"> 
        <h2>You are about to delete: {potName}</h2>
          <div className="row">
            <div className="col-lg-8">
              <p>Smart-Pot Name: {potName}</p>
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