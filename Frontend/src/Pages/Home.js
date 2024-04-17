import React, {useState, useEffect} from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
import Smartpot from '../Components/SmartPot';
import { getAllPots } from "../API/API_config";
//import HelpContent from '../Components/HelpPopUpContent.js';

const Home = () =>
{

     const [pots, setPots] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const potsData = await getAllPots();
            setPots(potsData);
        };
        fetchData();
    }, [])
    //const [showPopup, setShowPopup] = useState(false);

    //const togglePopup = () => {
  //setShowPopup(!showPopup);
  //};

    return (
        <div class="container-fluid">
            <div class="row">
                <div class="col-4 text-center">
                    <div>Profile</div>
                    
                    <Link to="/connect">
                        <button type="button" class="btn btn-outline-dark">Forbind plante</button>
                    </Link>
                    
                    
                    <Link to="/overview">
                    <button type="button" class="btn btn-outline-dark">Oversigt</button>
                    </Link>
                    

                </div>
                <div class="col-8">
                    <div>
                        <h1>planter</h1>
                        {pots.map((e) =>{
                        return(
                            <Smartpot pot={e}/>
                        );
                    }

                    )}
                    </div>
                </div>
            </div>
        </div>
      );
};


export default Home


