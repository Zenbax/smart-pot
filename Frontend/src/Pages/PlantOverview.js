import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../Styling/PlantOverview.css';
import PlantCreatePopUp from '../Components/PlantCreatePopUp';
import PlantTempContainer from '../Components/PlantTempContainer';
import EditPlantTemp from '../Components/EditPlantTemp';

const PlantOverview = () => {
  const [selectedTemplate, setSelectedTemplate] = useState('');
  const [showPopUp, setShowPopUp] = useState(false);

  // Function to handle popup actions
  const handlePopUpAction = () => {
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
    <div className="container overviewContainer">
      <div className="row">
        <div className="col-lg-7 h-100">
          <div className='row'>
            <div className='col-lg-1'>
              <button class="btn btn-secondary" onClick={handleBack}>Back</button>
            </div>
            <div className='col-lg-10'>
              <h1 className="text-center mb-4">Plants</h1>
            </div>
          </div>
          <PlantTempContainer onSelectTemplate={handleTemplateSelect} />
        </div>

        <div className="col-lg-1 d-flex align-items-center justify-content-center ">
          <div className="vertical-line d-none d-lg-block"></div>
          <div className="horisontal-line d-lg-none"></div>
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
