import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getPotFromId } from "../Util/API_config";
import PotDataChart from "../Components/PotDataChart";
import WaterContainerChart from "../Components/WaterContainerChart";
import EditPlantInPot from "../Components/EditPlantInPot";
import placeholder from "../images/no-image.jpeg"
import '../Styling/PotDetails.css'

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
        <div class='container'>
            <div class="row TopSpace">
                <div class='col-md-12'>
                    <h1>{pot?.nameOfPot || 'Loading...'}</h1>
                </div>
            </div>
            <div class='row'>
                <div class="col-md-8">
                    <div class='Row'>
                        <div class='col-md-12'>
                            <p class="Humidity">Humidity percentage: {latestMeasuredSoilData?.measuredSoilMoisture}</p>
                        </div>
                        <div class='col-md-12'>
                            {pot && <PotDataChart potID={potID} />}
                        </div>
                    </div>
                </div>
                <div class='col-md-4'>
                    <div class='row'>
                        <div class="col-md-9 ">
                            <img src={placeholder} alt="placeholder" class="img-fluid" />
                        </div>
                        <div class="col-md-3 align-self-end">
                            <WaterContainerChart currentWaterLevel={5} maxWaterLevel={20} />
                            Water in container
                        </div>
                        <div class='col-md-12'>
                            <div class='row'>
                                <div class='col-md-12 d-flex justify-content-between'>
                                    <button class='Buttons'>Disconnect plant</button>
                                    <button class='Buttons'>Change plant</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class='row'>
                <div class='col-md-7'>
                    <div class='row'>
                        <div class='col-md-12'>
                            <h3>Plant: {pot?.plant?.nameOfPlant ?? 'No plant assigned'}</h3>
                        </div>
                        <div class='col-md-12'>
                            <p class='WaterInfo'>Minimum Humidity: {pot?.plant?.soilMinimumMoisture ?? 'Not set'}</p>
                        </div>
                        <div class='col-md-12'>
                            <p class='WaterInfo'>mL per watering: {pot?.plant?.amountOfWaterToBeGiven ?? 'Not set'}</p>
                        </div>
                    </div>
                </div>
                <div class='col-md-5'>
                    <EditPlantInPot
                        handlePopUpAction={handlePopUpAction}
                        //initialMinMoisture={}
                        //initialWateringAmount={}
                        //plantName={Plant name}
                        //plantImage={Plant Image}
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
