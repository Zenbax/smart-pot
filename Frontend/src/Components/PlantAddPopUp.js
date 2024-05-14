import React from 'react';
import { Link } from 'react-router-dom';
import PlantTempContainer from '../Components/PlantTempContainer';
import '../Styling/PlantAddPopUp.css';

const PlantAddPopUp = ({ handlePopUpAction }) => {

    const handleTemplateSelect = (templateData) => {
        handlePopUpAction('add', templateData); // Pass the selected template data to the callback function
    };

    return (
        <div className="plantAdd-pop-up-container">
            <div className="plantAdd-pop-up">
                <div className="container-fluid">
                    <Link to="/plant_overview"><button className="create-button">Create New </button></Link>
                    <h1 className="text-center mb-4">Plants</h1>
                    <button onClick={() => handlePopUpAction('cancel')} className="cancel-button">Cancel</button>
                    <PlantTempContainer onSelectTemplate={handleTemplateSelect} />
                </div>
            </div>

        </div>
    );
};

export default PlantAddPopUp;