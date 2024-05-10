import React, { useState } from 'react';
import PlantTemp from '../Components/PlantTemplate';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../Styling/PlantOverview.css';
import PlantPopUp from '../Components/PlantPopUp';
import No_Image from '../images/no-image.jpeg'

const PlantOverview = () => {

  const [plantName, setPlantName] = useState('');
  const [minSoilMoisture, setMinSoilMoisture] = useState('');
  const [wateringAmount, setWateringAmount] = useState('');
  const [plantImage, setPlantImage] = useState(No_Image); // base64 string
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState(''); // 'create' or 'overwrite' or 'cancel'
  const [selectedTemplate, setSelectedTemplate] = useState(null); // Holds the data of selected template

  // Function to set PopUp to true
  const handleSubmit = (event) => {
    event.preventDefault();
    if (plantName != '' && minSoilMoisture != '' && wateringAmount > 20 && plantImage != No_Image) {
      setShowPopUp(true);
    }
    else if (wateringAmount <= 20){

    }
      
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    // Convert the image to base64
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onloadend = () => {
      setPlantImage(reader.result); // Set the base64 string as the plantImage
    };
  };

  const handlePopUpAction = (action) => {
    setPopUpAction(action);
    // Handle the action (create, overwrite, or cancel)
    console.log(`User chose to ${action}`);
    // Close the pop-up
    setShowPopUp(false);
  };

  // Function to handle selecting a template
  const handleTemplateSelect = (templateData) => {
    setSelectedTemplate(templateData);
    // Fill the input fields with data from the selected template
    setPlantName(templateData.name);
    setMinSoilMoisture(templateData.minSoilMoisture)
    setWateringAmount(templateData.wateringAmount)
    setPlantImage(templateData.image) //base64
    // You can fill other fields similarly
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-lg-7">
          <h1 className="text-center mb-4">Plants</h1>
          <div className="row">
            <div className="col-lg-4">
              <PlantTemp onSelectTemplate={handleTemplateSelect} />
            </div>
            <div className="col-lg-4">
              <PlantTemp onSelectTemplate={handleTemplateSelect} />
            </div>
            <div className="col-lg-4">
              <PlantTemp onSelectTemplate={handleTemplateSelect} />
            </div>
            <div className="col-lg-4">
              <PlantTemp onSelectTemplate={handleTemplateSelect} />
            </div>
            <div className="col-lg-4">
              <PlantTemp onSelectTemplate={handleTemplateSelect} />
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
                {plantImage && (
                  <div className="image-preview-container">
                    <img src={plantImage} alt="Plant" className="image-preview" />
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
          plantImage={plantImage}
        />
      )}

    </div>
  );
};

export default PlantOverview;
