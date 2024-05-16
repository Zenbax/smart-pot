import React, { useState, useEffect } from 'react';
import PlantCreatePopUp from './PlantCreatePopUp';

const EditPlantTemp = ({ handlePopUpAction, initialMinMoisture, initialWateringAmount, plantName, plantImage }) => {
  const [minSoilMoisture, setMinSoilMoisture] = useState(initialMinMoisture);
  const [wateringAmount, setWateringAmount] = useState(initialWateringAmount);
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState('');

  useEffect(() => {
    setMinSoilMoisture(initialMinMoisture || '');
    setWateringAmount(initialWateringAmount || '');
  })

  const handleSubmit = (event) => {
    event.preventDefault();
    if (minSoilMoisture !='' && wateringAmount > 19 && wateringAmount < 251 && minSoilMoisture !==initialMinMoisture && wateringAmount !==initialWateringAmount) {
      setShowPopUp(true);
    } else if (wateringAmount <= 19) {
      // Todo: handle invalid input
    }
  };

  const handlePopUpActionLocal = (action) => {
    setPopUpAction(action);
    console.log(`User chose to ${action}`);
    setShowPopUp(false);
    handlePopUpAction(action);
  };

  return (
    <div className="plant-temp-container">
      <h2>Edit Plant</h2>
      <form className="plant-temp-form" onSubmit={handleSubmit}>
        <div className="plant-input-grid">
          <label>Minimum Soil Moisture:</label>
          <div className="plant-input-container">
            <input
              id="minSoilMoistureInput"
              type="text"
              placeholder="Enter minimum moisture"
              value={minSoilMoisture}
              onChange={(e) => setMinSoilMoisture(e.target.value)}
            />
          </div>
          <label>Watering Amount (ml):</label>
          <div className="plant-input-container">
            <input
              id="wateringAmountInput"
              type="number"
              placeholder="Enter watering amount"
              value={wateringAmount}
              onChange={(e) => setWateringAmount(e.target.value)}
            />
          </div>
        </div>
        <button type="submit" className="submit-button">Save Changes</button>
      </form>
      {showPopUp && (
        <PlantCreatePopUp
          handlePopUpAction={handlePopUpActionLocal}
          plantName={plantName}
          minSoilMoisture={minSoilMoisture}
          wateringAmount={wateringAmount}
          plantImage={plantImage}
        />
      )}
    </div>
  );
};

export default EditPlantTemp;
