import React, { useState } from 'react';
import PlantTempContainer from '../Components/PlantTempContainer';
import '../Styling/PlantAddPopUp.css';
import { useNavigate } from 'react-router-dom';

const PlantAddPopUp = ({ handlePopUpAction, ShowRemove }) => {
    const [showRemove, setShowRemove] = useState(ShowRemove);
    const navigate = useNavigate();

    const handleTemplateSelect = (templateData) => {
        handlePopUpAction('add', templateData); // Pass the selected template data to the callback function
    };

    const handleCancel = () => {
        handlePopUpAction('cancel');
    };

    const handleCreateNew = () => {
        navigate('/smart-pot/plant_overview');
    };

    return (
        <div className="plantAdd-pop-up-container">
            <div className="plantAdd-pop-up" data-testid="plant-add-popup">
                <div className="container-fluid">
                <button className="create-button" onClick={handleCreateNew}>Create New</button>
                    <h1 className="text-center mb-4">Plants</h1>
                    {showRemove && (
                        <button onClick={() => handlePopUpAction('remove')} className="remove-button">Remove plant</button>
                    )}
                    <button onClick={handleCancel} className="cancel-button">Cancel</button>
                    <PlantTempContainer onSelectTemplate={handleTemplateSelect} />
                </div>
            </div>
        </div>
    );
};

export default PlantAddPopUp;
