import React, { useState } from 'react';
import PlantAddPopUp from '../Components/PlantAddPopUp';

const Connect = () => {
    // State variables for username and password
    const [idOfPot, setPotID] = useState('');
    const [nameOfPot, setPotName] = useState('');
    const [showPopUp, setShowPopUp] = useState(false);

    // Function to handle form submission
    const handleSubmit = (event) => {
        event.preventDefault();
        // Here you can perform login logic using username and password
        // For example, sending a request to your server
    };

    const handlePopUpAction = (action) => {
        // Close the pop-up
        setShowPopUp(false);
        // Handle the action (create, overwrite, or cancel)
        console.log(`User chose to ${action}`);
    };

      const handleAddPlantClick = () => {
        setShowPopUp(true); // Show the popup when "Add a plant" is clicked
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

                <div className="image-edit-container">
                    <label className="file-upload-button" onClick={handleAddPlantClick}>
                        Add a plant
                    </label>
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

export default Connect;




