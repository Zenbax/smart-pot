import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { getPotFromId } from "../API/API_config";
import PotDataChart from "../Components/PotDataChart";

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
        <div class='row'>
            <div class="col-md-8">
                <div class='row'>
                    <div class='col-md-12'>
                        <h1>{pot?.NameOfpot} test</h1>
                    </div>
                    <div class='col-md-12'>
                        humidity percentage
                    </div>
                </div>
            </div>
            <div class='col-md-4'>
                <div class='row'>
                    <div class="col-md-9">
                        <img></img>
                    </div>
                    <div class="col-md-3">
                        water
                    </div>
                </div>  
            </div>
        <div class='row'>
            <div class='col-lg-8'>
            {pot && <PotDataChart potID={potID} />}
            aaaaaaaaaaaaaaaaaaaaah
            </div>
                <div class='col-lg-4'>
                    <div class='row'>
                        <div class='col-lg-12'>
                            <button></button>
                        </div>
                        <div class='col-lg-12'>
                            <button></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class='row'>
            <div class='col-md-7'>
                <div class='row'>
                    <div class='col-md-12'>
                        Plant:
                    </div>
                    <div class='col-md-12'>
                        Minimum Humidity:
                    </div>
                    <div class='col-md-12'>
                        mL per watering:
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
