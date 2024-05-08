import React from 'react';

const PlantPopUp = ({ handlePopUpAction }) => {
  return (
    <div className="custom-pop-up-container">
      <div className="custom-pop-up">
        <p>Do you want to create a new plant or overwrite an existing one?</p>
        <button onClick={() => handlePopUpAction('create')} className="create-button">Create New</button>
        <button onClick={() => handlePopUpAction('overwrite')} className="overwrite-button">Overwrite Existing</button>
      </div>
    </div>
  );
};

export default PlantPopUp;
