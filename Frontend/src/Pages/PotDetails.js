import React, { useEffect, useState } from "react";

import { useNavigate, useParams } from "react-router-dom"
import { deletePot, getPotFromId, updatePlant, updatePot } from "../Util/API_config";
import PotDataChart from "../Components/PotDataChart";
import WaterContainerChart from "../Components/WaterContainerChart";
import EditPlantInPot from "../Components/EditPlantInPot";
import PlantAddPopUp from "../Components/PlantAddPopUp";
import DeletePopUp from '../Components/DeletePotPopUp';
import placeholder from "../images/no-image.jpeg";
import '../Styling/PotDetails.css';
import { useAuth } from "../Util/AuthProvider";


export default function PotDetails() {
    const { potID } = useParams();
    const navigate = useNavigate();
    const { setToken } = useAuth();
    const [pot, setPot] = useState();
    const [latestMeasuredSoilData, setLatestMeasuredSoilData] = useState(null);
    const [popupType, setPopupType] = useState(false);
    const handleNotAuthorized = () => {
        console.log("Removing token")
        setToken("");
    }
    const handlePotNotFound = () => {
        console.log("not found error")
        navigate('/pot-details/');
    }


    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await getPotFromId(potID, setToken, handlePotNotFound);
                if (response.success) {
                    setPot(response.pot);
                    const sensorData = response.pot.sensorData;
                    if (sensorData && sensorData.length > 0) {
                        // Get the latest measured soil data
                        const latestData = sensorData[sensorData.length - 1];
                        setLatestMeasuredSoilData(latestData);
                    }
                } else {
                    console.error('Error fetching pot data:', response.message);
                }
            } catch (error) {
                //console.error('Error fetching pot data:', error);
                handlePotNotFound
            }
        };
        fetchData();
    }, [potID]);

    const handleDisconnect = async () => {
        setPopupType("delete");
    };


    const handleChangePlant = () => {
        setPopupType("change");
    };

    const handlePopUpAction = async (action, templateData = null) => {
        setPopupType('');
        console.log(`User chose to ${action} ${templateData.nameOfPlant}`);
        if (action === 'add' && templateData) {
            try {
                await updatePot(pot.nameOfPot, pot.email, potID, pot.enable, templateData, potID, setToken);
                await setPot((prevPot) => ({ ...prevPot, plant: templateData }));

            } catch (error) {
                console.error('Error updating pot:', error.message);
            }
        }

        else if (action === 'remove') {
            try {
                await updatePot(pot.nameOfPot, pot.email, potID, pot.enable, null, potID, setToken);
                setPot((prevPot) => ({ ...prevPot, plant: null }));
            } catch (error) {
                console.error('Error removing plant from pot:', error.message);
            }
        }

        else if (action === 'delete') {
            try {
                await deletePot(potID, setToken);
                navigate("/");
            } catch (error) {
                console.error('Error disconnecting pot:', error);
                return;
            }
        }
    };

    return (
        <div className='container'>
            <div className="row TopSpace">
                <div className='col-md-12'>
                    <h1>{pot?.nameOfPot || 'Loading...'}</h1>
                </div>
            </div>
            <div className='row'>
                <div className="col-md-8">
                    <div className='Row'>
                        <div className='col-md-12'>
                            <p className="Humidity">Humidity percentage: {latestMeasuredSoilData?.measuredSoilMoisture}</p>
                        </div>
                        <div className='col-md-12'>
                            {pot && <PotDataChart potID={potID} />}
                        </div>
                    </div>
                </div>
                <div className='col-md-4'>
                    <div className='row'>
                        <div className="col-md-9 ">
                            <img src={pot?.plant?.image ?? placeholder} alt="placeholder" className="img-fluid" />
                        </div>
                        <div className="col-md-3 align-self-end">
                            {latestMeasuredSoilData && (
                                <WaterContainerChart
                                    currentWaterLevel={latestMeasuredSoilData.waterTankLevel}
                                    maxWaterLevel={100} // Assuming 100 is the max level
                                />
                            )}
                        </div>
                        <div className='col-md-12'>
                            <div className='row'>
                                <div className='col-md-12 d-flex justify-content-between'>
                                    <button className='Buttons' onClick={handleDisconnect}>Disconnect pot</button>
                                    <button className='Buttons' onClick={handleChangePlant}>Change plant</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className='row'>
                <div className='col-md-7'>
                    <div className='row'>
                        <div className='col-md-12'>
                            <h3>Plant: {pot?.plant?.nameOfPlant ?? 'No plant assigned'}</h3>
                        </div>
                        <div className='col-md-12'>
                            <p className='WaterInfo'>Minimum Humidity: {pot?.plant?.soilMinimumMoisture ?? 'Not set'}</p>
                        </div>
                        <div className='col-md-12'>
                            <p className='WaterInfo'>mL per watering: {pot?.plant?.amountOfWaterToBeGiven ?? 'Not set'}</p>
                        </div>
                    </div>
                </div>
                <div className='col-md-5'>
                    <EditPlantInPot
                        handlePopUpAction={handlePopUpAction}
                        plant={pot?.plant}
                    />
                </div>
            </div>

            {popupType === 'change' && (
                <PlantAddPopUp
                    handlePopUpAction={handlePopUpAction}
                    ShowRemove={true}
                />
            )}

            {popupType === 'delete' && (
                <DeletePopUp
                    handlePopUpAction={handlePopUpAction}
                    potName={pot.nameOfPot}
                    plantImage={pot.plant.image}
                />
            )}

        </div>
    );
}
