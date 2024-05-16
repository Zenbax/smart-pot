import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getPotFromId } from "../API/API_config";
import PotDataChart from "../Components/PotDataChart";
import WaterContainerChart from "../Components/WaterContainerChart";
import EditPlantInPot from "../Components/EditPlantInPot";
import placeholder from "../images/no-image.jpeg";
import '../Styling/PotDetails.css';

export default function PotDetails() {
    const { potID } = useParams();
    const [pot, setPot] = useState();
    const [latestMeasuredSoilData, setLatestMeasuredSoilData] = useState(null);
    const [showPopUp, setShowPopUp] = useState(false);
    const [popUpAction, setPopUpAction] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await getPotFromId(potID);
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
                console.error('Error fetching pot data:', error);
            }
        };
        fetchData();
    }, [potID]);

    const handlePopUpAction = (action) => {
        setPopUpAction(action);
        // Handle the action (create, overwrite, or cancel)
        console.log(`User chose to ${action}`);
        setShowPopUp(false);
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
                            <img src={placeholder} alt="placeholder" className="img-fluid" />
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
                                    <button className='Buttons'>Disconnect plant</button>
                                    <button className='Buttons'>Change plant</button>
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
                    />
                </div>
            </div>

            {showPopUp && (
                <PlantCreatePopUp
                    handlePopUpAction={handlePopUpAction}
                />
            )}

        </div>
    );
}
