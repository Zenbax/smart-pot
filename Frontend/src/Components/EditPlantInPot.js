import React, { useState, useEffect } from 'react';
import PlantCreatePopUp from './PlantCreatePopUp';
import { createPlant, updatePlant, getPlantByName } from "../Util/API_config";
import { useAuth } from '../Util/AuthProvider';

const EditPlantTemp = ({ handlePopUpAction, plant }) => {
  const [plantName, setPlantName] = useState(plant?.nameOfPlant || '');
  const [minSoilMoisture, setMinSoilMoisture] = useState(plant?.soilMinimumMoisture || '');
  const [wateringAmount, setWateringAmount] = useState(plant?.amountOfWaterToBeGiven || '');
  const [plantImage, setPlantImage] = useState(plant?.image || '');

  const isDefault = plant?.isDefault || '';
  const initialMinMoisture = plant?.soilMinimumMoisture || '';
  const initialWateringAmount = plant?.amountOfWaterToBeGiven || '';

  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState('');
  const [showError, setShowError] = useState(false);
  const [error, setError] = useState('');
  const { setToken } = useAuth();

  useEffect(() => {
    setPlantName(plant?.nameOfPlant || '');
    setMinSoilMoisture(plant?.soilMinimumMoisture || '');
    setWateringAmount(plant?.amountOfWaterToBeGiven || '');
    setPlantImage(plant?.image || '');
  }, [plant]);

  const handleWateringAmountChange = (e) => {
    const value = e.target.value;
    if (value === '') {
      setWateringAmount('');
    } else if (parseInt(value, 10) > 250) {
      setWateringAmount(250);
    } else if (parseInt(value, 10) < 0) {
      setWateringAmount(0);
    } else {
      setWateringAmount(parseInt(value, 10));
    }
  };

  const handleMinMoistureChange = (e) => {
    const value = e.target.value;
    if (value === '') {
      setMinSoilMoisture('');
    } else if (parseInt(value, 10) > 100) {
      setMinSoilMoisture(100);
    } else if (parseInt(value, 10) < 0) {
      setMinSoilMoisture(0);
    } else {
      setMinSoilMoisture(parseInt(value, 10));
    }
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    if (minSoilMoisture === '' || wateringAmount === '') {
      setError('All fields must be filled');
      setShowError(true);
      return;
    }

    const minSoilMoistureInt = parseInt(minSoilMoisture, 10);
    const wateringAmountInt = parseInt(wateringAmount, 10);
    const initialMinMoistureInt = parseInt(initialMinMoisture, 10);
    const initialWateringAmountInt = parseInt(initialWateringAmount, 10);

    if (wateringAmountInt < 20) {
      setError('Watering amount must be 20ml or higher');
      setShowError(true);
    } else if (minSoilMoistureInt === initialMinMoistureInt && wateringAmountInt === initialWateringAmountInt) {
      setError('Values have not changed');
      setShowError(true);
    } else if (
      minSoilMoisture !== '' &&
      wateringAmountInt > 19 &&
      wateringAmountInt < 251 &&
      (minSoilMoistureInt !== initialMinMoistureInt || wateringAmountInt !== initialWateringAmountInt)
    ) {
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
        await createPlant("new " + plantName, minSoilMoisture, plantImage, wateringAmount, setToken);
        const templateData = await getPlantByName("new " + plantName, setToken);
        handlePopUpAction('add', templateData.plant);
      } catch (error) {
        console.error('Error creating plant:', error.message);
      }
    }

    if (action === 'overwrite') {
      try {
        await updatePlant(plantName, minSoilMoisture, wateringAmount, plantImage, plantName, setToken);
        const templateData = await getPlantByName(plantName, setToken);
        handlePopUpAction('add', templateData.plant);
      } catch (error) {
        console.error('Error updating plant:', error.message);
      }
    }
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
          isDefault={isDefault}
        />
      )}
    </div>
  );
};

export default EditPlantTemp;
