import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getPotFromId } from "../API/API_config";
import PotDataChart from "../Components/PotDataChart";
import WaterContainerChart from "../Components/WaterContainerChart";
import placeholder from "../images/no-image.jpeg"
import '../Styling/PotDetails.css'

export default function PotDetails(){
    const {potID} = useParams();
    const [pot, setPot] = useState();

  useEffect(() => {
    const fetchData = async () => {
        const potData = await getPotFromId(potID);
        setPot(potData);
    };
    fetchData();
    console.log(pot);
}, [])



    return(
    <div class='container'>
        <div class="row TopSpace">
            <div class='col-md-12'>
                <h1>{pot?.NameOfpot}Pot name placeholder</h1>
            </div>
        </div>
        <div class='row'>
            <div class="col-md-8">
                <div class='Row'>
                    <div class='col-md-12'>
                        <p class="Humidity">Humidity percentage: </p>
                    </div>
                    <div class='col-md-12'>
                        {pot && <PotDataChart potID={potID} />}
                    </div>
                </div>
            </div>
            <div class='col-md-4'>
                <div class='row'>
                    <div class="col-md-9 ">
                        <img src={placeholder} alt="placeholder" class="img-fluid"/> 
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
                            <h3>Plant:</h3>
                        </div>
                        <div class='col-md-12'>
                            <p class='WaterInfo'>Minimum Humidity:</p>
                        </div>
                        <div class='col-md-12'>
                            <p class='WaterInfo'>mL per watering:</p>
                        </div>
                    </div>
                </div>
                <div class='col-md-5'>
                Component: Edit humidity and water
                </div>
            </div>
        
    </div>
    );
}
