import React, { useState, useEffect } from 'react';
import PlantCreatePopUp from './PlantCreatePopUp';
import DeletePopUp from './DeletePlantPopUp';
import No_Image from '../images/no-image.jpeg';
import { createPlant, updatePlant, deletePlant } from "../Util/API_config";
import { useAuth } from '../Util/AuthProvider';

const EditPlantTemp = ({ selectedTemplate, handlePopUpAction }) => {
  const [plantName, setPlantName] = useState('');
  const { setToken } = useAuth();
  const [plantInitialName, setPlantInitialName] = useState('');
  const [minSoilMoisture, setMinSoilMoisture] = useState('');
  const [wateringAmount, setWateringAmount] = useState('');
  const [plantImage, setPlantImage] = useState(No_Image);
  const [isDefault, setIsDefault] = useState(true);
  const [popupType, setPopupType] = useState('');
  const [popUpAction, setPopUpAction] = useState('');
  const [showError, setShowError] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    if (selectedTemplate) {
      setPlantName(selectedTemplate.nameOfPlant || '');
      setPlantInitialName(selectedTemplate.nameOfPlant || '');
      setMinSoilMoisture(selectedTemplate.soilMinimumMoisture || '');
      setWateringAmount(selectedTemplate.amountOfWaterToBeGiven || '');
      setPlantImage(selectedTemplate.image || No_Image);
      setIsDefault(selectedTemplate.isDefault || false);
    }
  }, [selectedTemplate]);

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

    if (plantName === '' || minSoilMoisture === '' || wateringAmount === '') {
      setError('All fields must be filled');
      setShowError(true);
    } else if (plantImage === No_Image) {
      setError('Plant must have an Image');
      setShowError(true);
    } else if (wateringAmount < 20) {
      setError('Watering amount must be 20ml or higher');
      setShowError(true);
    } else if (plantName !== '' && minSoilMoisture !== '' && plantImage !== No_Image && wateringAmount > 19 && wateringAmount < 251) {
      handleCloseError();
      console.log("Showing Create PopUp")
      setPopupType('create');
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onloadend = () => {
      setPlantImage(reader.result);
    };
  };

  const handlePopUpActionLocal = async (action) => {
    setPopUpAction(action);
    console.log(`User chose to ${action}`);
    setPopupType(''); // Close the popup
    if (action === 'create') {
        const response =await createPlant("new " + plantName, minSoilMoisture, plantImage, wateringAmount, setToken);
        if (response === true){
          window.location.reload();
        }
        else{
          setError(response);
          setShowError(true);
        }
    }

    else if (action === 'overwrite' && isDefault === false) {
        const response = await updatePlant(plantName, minSoilMoisture, wateringAmount, plantImage, plantInitialName, setToken);
        if (response === true){
          window.location.reload();
        }
        else{
          setError(response);
          setShowError(true);
        }
        
    }

    else if (action === 'delete' && isDefault === false) {
        const response = await deletePlant(plantInitialName, setToken);
        window.location.reload();
        if (response === true){
          window.location.reload();
        }
        else{
          setError(response);
          setShowError(true);
        }
    }
    handlePopUpAction(action);
  };

  const handleDeletePopup = (e) => {
    e.preventDefault();
    setPopupType('delete');
  };

  const handleCloseError = () => {
    setShowError(false);
    setError('');
  };

  return (
    <div className="plant-temp-form-container">
      <h2>{selectedTemplate ? 'Edit Plant' : 'Create Plant'}</h2>
      <form className="plant-temp-form" onSubmit={handleSubmit}>
        <div className="plant-input-grid">
          <label>Plant Name:</label>
          <div className="plant-input-container">
            <input
              id="plantNameInput"
              type="text"
              placeholder="Enter plant name"
              value={plantName}
              onChange={(e) => setPlantName(e.target.value)}
            />
          </div>
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
        <div className="image-edit-container">
          <label className="file-upload-button">
            Upload a picture
            <input
              type="file"
              accept="image/*"
              onChange={handleImageChange}
            />
          </label>
          {plantImage && (
            <div className="image-preview-container">
              <img src={plantImage} alt="Plant" className="image-preview" />
            </div>
          )}
        </div>

        {showError && (
          <div className='error-popup'>
            <p>{error}</p>
          </div>
        )}

        <div className='Edit-Button-Container'>
          <button type="submit" className="btn btn-success">Save Changes</button>
          {!isDefault && (
            <button onClick={handleDeletePopup} className="btn btn-danger">Delete Plant</button>
          )}
        </div>
      </form>

      {popupType === 'create' && (
        <PlantCreatePopUp
          handlePopUpAction={handlePopUpActionLocal}
          plantName={plantName}
          minSoilMoisture={minSoilMoisture}
          wateringAmount={wateringAmount}
          plantImage={plantImage}
          isDefault={isDefault}
        />
      )}

      {popupType === 'delete' && (
        <DeletePopUp
          handlePopUpAction={handlePopUpActionLocal}
          plantName={plantInitialName}
          minSoilMoisture={minSoilMoisture}
          wateringAmount={wateringAmount}
          plantImage={plantImage}
        />
      )}

    </div>
  );
};

export default EditPlantTemp;
