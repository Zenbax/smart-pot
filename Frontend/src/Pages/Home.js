import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
//import HelpContent from '../Components/HelpPopUpContent.js';

const Home = () =>
{

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
                    <div>planter</div>
                </div>
            </div>
        </div>
      );
};


export default Home

