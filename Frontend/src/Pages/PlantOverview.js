import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../Styling/PlantOverview.css';
import PlantCreatePopUp from '../Components/PlantCreatePopUp';
import PlantTempContainer from '../Components/PlantTempContainer';
import EditPlantTemp from '../Components/EditPlantTemp';

const PlantOverview = () => {
  const [selectedTemplate, setSelectedTemplate] = useState('');
  const [showPopUp, setShowPopUp] = useState(false);
  const [popUpAction, setPopUpAction] = useState('');

  // Function to handle popup actions
  const handlePopUpAction = (action) => {
    setPopUpAction(action);
    // Handle the action (create, overwrite, or cancel)
    console.log(`User chose to ${action}`);
    setShowPopUp(false);
  };

  const handleBack = () => {
    window.history.back();
  };

  // Function to handle selecting a template
  const handleTemplateSelect = (templateData) => {
    setSelectedTemplate(templateData);
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-lg-7">
          <button onClick={handleBack}>Back</button>
          <h1 className="text-center mb-4">Plants</h1>
          <PlantTempContainer onSelectTemplate={handleTemplateSelect} />
        </div>

        <div className="col-lg-1 d-flex align-items-center justify-content-center">
          <div className="vertical-line"></div>
        </div>

        <div className="col-lg-4 d-flex align-items-center justify-content-center">
          <EditPlantTemp selectedTemplate={selectedTemplate} handlePopUpAction={handlePopUpAction} />
        </div>
      </div>

      {showPopUp && (
        <PlantCreatePopUp handlePopUpAction={handlePopUpAction} />
      )}
    </div>
  );
};

export default PlantOverview;
