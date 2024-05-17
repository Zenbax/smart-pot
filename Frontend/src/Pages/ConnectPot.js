import React, { useState } from 'react';
import PlantAddPopUp from '../Components/PlantAddPopUp';
import { createPot } from "../Util/API_config";
import { useNavigate } from 'react-router-dom';

const ConnectPot = () => {
    const [idOfPot, setPotID] = useState('');
    const [nameOfPot, setPotName] = useState('');
    const [plantData, setPlantData] = useState(null); // State for selected plant data
    const [showPopUp, setShowPopUp] = useState(false);
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState('');

    const navigate = useNavigate();

    const handleSubmit = (event) => {
        event.preventDefault();

        handleCloseError();

        if (!idOfPot || !nameOfPot) {
            setError('Smart-pot ID and Pot name are required');
            setShowError(true);
            return;
        }

        
        createPot(nameOfPot, idOfPot, plantData)

        navigate("/");

    };

    const handlePopUpAction = (action, plantData) => {
        setShowPopUp(false);

        if (action === 'add' && plantData) {
            setPlantData(plantData);
        }
    };

    const handleAddPlantClick = () => {
        setShowPopUp(true);
    };   
    
    const handleCloseError = () => {
        setShowError(false);
        setError('');
    };

    return (
        <div className="container">
            <h2 className="h2"> Connect your Smart-pot </h2>
            <form className="form" onSubmit={handleSubmit}>
                <div className="input-container">
                    <input
                        type="text"
                        placeholder="Smart-pot ID"
                        value={idOfPot}
                        onChange={(e) => setPotID(e.target.value)}
                    />
                </div>
                <div className="input-container">
                    <input
                        type="text"
                        placeholder="Name your pot"
                        value={nameOfPot}
                        onChange={(e) => setPotName(e.target.value)}
                    />
                </div>

                {showError && (
                <div className='error-popup'>
                    <p>{error}</p>
                </div>
                )}

                <div>
                    <label className="file-upload-button" onClick={handleAddPlantClick}>
                        Add a plant
                    </label>
                </div>

                <div className="added-plant-container">
                    {plantData && (
                        <div className="col-lg-8">
                            <p>Plant Name: {plantData.nameOfPlant}</p>
                            <p>Minimum Soil Moisture: {plantData.soilMinimumMoisture}</p>
                            <p>Watering Amount (ml): {plantData.amountOfWaterToBeGiven}</p>
                        </div>
                    )}
                    {plantData && (
                        <div className="col-lg-4">
                            <img src={plantData.image} alt="Plant Preview" className="image-preview" />
                        </div>
                    )}
                </div>

                <button type="submit">Connect Smart-pot</button>
            </form>

            {showPopUp && (
                <PlantAddPopUp
                    handlePopUpAction={handlePopUpAction}
                />
            )}
        </div>
    );
}

export default ConnectPot;
