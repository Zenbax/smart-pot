import React, { useState, useEffect } from 'react';
import PlantCreatePopUp from './PlantCreatePopUp';
import { createPlant, updatePlant } from "../Util/API_config";

const EditPlantTemp = ({ handlePopUpAction, plant}) => {
  const [plantName, setPlantName] = useState('');
  const [minSoilMoisture, setMinSoilMoisture] = useState('');
  const [wateringAmount, setWateringAmount] = useState('');
  const [plantImage, setPlantImage] = useState('');
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState('');
  const [showError, setShowError] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    setPlantName(plant?.nameOfPlant || '')
    setMinSoilMoisture(plant?.soilMinimumMoisture || '');
    setWateringAmount(plant?.amountOfWaterToBeGiven || '');
    setPlantImage(plant?.image || '')
  }, [plant]);

  const handleWateringAmountChange = (e) => {
    const value = e.target.value;
    if (value > 250) {
      setWateringAmount(250);
    } else if (value < 0) {
      setWateringAmount(0);
    } else {
      setWateringAmount(value);
    }
  };

  const handleMinMoistureChange = (e) => {
    const value = e.target.value;
    if (value > 100) {
      setMinSoilMoisture(100);
    } else if (value < 0) {
      setMinSoilMoisture(0);
    } else {
      setMinSoilMoisture(value);
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (minSoilMoisture === '' || wateringAmount === '') {
      setError('All fields must be filled');
      setShowError(true);
    }
    if (wateringAmount < 20) {
      setError('Watering amount must be 20ml or higher');
      setShowError(true);
      } 
    if (minSoilMoisture !='' && wateringAmount > 19 && wateringAmount < 251 && (minSoilMoisture !=plant?.soilMinimumMoisture || wateringAmount !=plant?.amountOfWaterToBeGiven)) {
      handleCloseError();
      setShowPopUp(true);
    }
  };

  const handlePopUpActionLocal = async (action) => {
    setPopUpAction(action);
    console.log(`User chose to ${action}`);
    setShowPopUp(false);
    if (action === 'create') {
      try {
        await createPlant(plantName, minSoilMoisture, plantImage, wateringAmount);
      } catch (error) {
        console.error('Error creating plant:', error.message);
      }
    }

    if (action === 'overwrite') {
      try {
        await updatePlant(plantName, minSoilMoisture, wateringAmount, plantImage, plantName);
      } catch (error) {
        console.error('Error creating plant:', error.message);
      }
    }
    handlePopUpAction(action);
  };

  const handleCloseError = () => {
    setShowError(false);
    setError('');
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
              type="number"
              placeholder="Enter minimum moisture"
              value={minSoilMoisture}
              onChange={handleMinMoistureChange}
            />
          </div>
          <label>Watering Amount (ml):</label>
          <div className="plant-input-container">
            <input
              id="wateringAmountInput"
              type="number"
              placeholder="Enter watering amount"
              value={wateringAmount}
              onChange={handleWateringAmountChange}
            />
          </div>
        </div>

        {showError && (
          <div className='error-popup'>
            <p>{error}</p>
          </div>
        )}

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
