import React, { useState } from 'react';
import PlantTemp from '../Components/PlantTemplate';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../Styling/PlantOverview.css';
import PlantPopUp from '../Components/PlantPopUp';

const PlantOverview = () => {

  const defaultImageURL = 'https://static.vecteezy.com/system/resources/previews/003/193/486/original/cute-cartoon-home-plant-in-clay-pot-illustration-vector.jpg';

  const [plantName, setPlantName] = useState('');
  const [minSoilMoisture, setMinSoilMoisture] = useState('');
  const [wateringAmount, setWateringAmount] = useState('');
  const [plantImage, setPlantImage] = useState(''); // holds a file object
  const [imagePreview, setImagePreview] = useState(defaultImageURL); // holds a url to be shown
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState(''); // 'create' or 'overwrite' or 'cancel'

  // Function to set PopUp to true
  const handleSubmit = (event) => {
    event.preventDefault();
    if(plantName !='' && minSoilMoisture !='' && wateringAmount > 0){
      setShowPopUp(true);
    }
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setPlantImage(file);
    setImagePreview(URL.createObjectURL(file)); // Create a preview URL for the image
    // der sker en fejl ved createObjectURL, hvis man allrade har uploadet en fil og så prøver igen men annullere
  };

  const handlePopUpAction = (action) => {
    setPopUpAction(action);
    // Here you can handle the action (create or overwrite)
    console.log(`User chose to ${action}`);
    // Close the pop-up
    setShowPopUp(false);
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-lg-7">
          <h1 className="text-center mb-4">Plants</h1>
          <div className="row">
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
          </div>
        </div>

        <div className="col-lg-1 d-flex align-items-center justify-content-center">
          <div className="vertical-line"></div>
        </div>


        <div className="col-lg-4 d-flex align-items-center justify-content-center">
          <div className="plant-temp-container">
            <h2> Edit Plant </h2>
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
                {imagePreview && (
                  <div className="image-preview-container">
                    <img src={imagePreview} alt="Plant" className="image-preview" />
                  </div>
                )}
              </div>

              <button type="submit" className="submit-button">Save Changes</button>

            </form>
          </div>
        </div>
      </div>

      {showPopUp && (
        <PlantPopUp
          handlePopUpAction={handlePopUpAction}
          plantName={plantName}
          minSoilMoisture={minSoilMoisture}
          wateringAmount={wateringAmount}
          imagePreview={imagePreview}
        />
      )}

    </div>
  );
};

export default PlantOverview;
