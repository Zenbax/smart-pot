import React, { useState, useEffect } from 'react';
import PlantCreatePopUp from './PlantCreatePopUp';
import No_Image from '../images/no-image.jpeg';

const EditPlantTemp = ({ selectedTemplate, handlePopUpAction }) => {
  const [plantName, setPlantName] = useState('');
  const [minSoilMoisture, setMinSoilMoisture] = useState('');
  const [wateringAmount, setWateringAmount] = useState('');
  const [plantImage, setPlantImage] = useState(No_Image);
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState('');

  useEffect(() => {
    if (selectedTemplate) {
      setPlantName(selectedTemplate.name || '');
      setMinSoilMoisture(selectedTemplate.minSoilMoisture || '');
      setWateringAmount(selectedTemplate.wateringAmount || '');
      setPlantImage(selectedTemplate.image || No_Image);
    }
  }, [selectedTemplate]);

  const handleSubmit = (event) => {
    event.preventDefault();
    if (plantName && minSoilMoisture && plantImage !== No_Image && wateringAmount > 19 && wateringAmount < 251) {
      setShowPopUp(true);
    } else if (wateringAmount <= 19) {
      // Todo: handle invalid input
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
