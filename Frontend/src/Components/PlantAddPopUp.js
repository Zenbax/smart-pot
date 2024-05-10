import React, { useState } from 'react';
import PlantTemp from '../Components/PlantTemplate';
import '../Styling/PlantAddPopUp.css';

const PlantCreatePopUp = ({ handlePopUpAction, plantName, minSoilMoisture, wateringAmount, plantImage }) => {

    const [selectedTemplate, setSelectedTemplate] = useState(null); // Holds the data of selected template

    const handleTemplateSelect = (templateData) => {
        setSelectedTemplate(templateData);
        // Fill the input fields with data from the selected template
    };

    return (
        <div className="plantAdd-pop-up-container">
            <div className="plantAdd-pop-up">
                <div className="container-fluid">
                    <button onClick={() => handlePopUpAction('create')} className="create-button">Create New</button>
                    <h1 className="text-center mb-4">Plants</h1>
                    <button onClick={() => handlePopUpAction('cancel')} className="cancel-button">Cancel</button>
                    <div className="row">
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                        <div className="col-lg-3">
                            <PlantTemp onSelectTemplate={handleTemplateSelect} />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default PlantCreatePopUp;