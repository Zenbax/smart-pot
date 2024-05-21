import React, {useState, useEffect} from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
import {Container, Row, Col} from 'react-bootstrap';
import PotsList from "../Components/PotsList";
import { getAllPots } from "../Util/API_config";
import '../Styling/Home.css'
import { useAuth } from "../Util/AuthProvider";
//import HelpContent from '../Components/HelpPopUpContent.js';
const Home = () =>
{
    const { token, setToken } = useAuth();
     const [pots, setPots] = useState([]);

    useEffect(() => {
        if(token){
        const fetchData = async () => {
            const potsData = await getAllPots(setToken);
            setPots(potsData);
        };
        fetchData();
        }
        
    }, [token])
  
    //const [showPopup, setShowPopup] = useState(false);

    //const togglePopup = () => {
  //setShowPopup(!showPopup);
  //};

    return (
        <Container className="homeContainer">
            <Row className="flex-column h-100">
                <Col md="4">
                    <Row>
                        <div>Profile</div>
                    </Row>
                    <Row>
                        <div>
                            <button type="button" className="btn btn-outline-dark">
                                <Link to="/connect_pot">Connect pot</Link>
                            </button>        
                        </div>
                        <div>
                            <button type="button" className="btn btn-outline-dark">
                                <Link to="/plant_overview">Plant Overview</Link>
                            </button>
                        </div>
                    </Row>
                </Col>
                <Col md="8" className="flex-grow-1 h-100">
                    <Row>
                        <h1>Pots</h1>
                    </Row>
                    <Row className=" flex-grow-1 homeRow" >
                        {pots && pots.length ?
                        <PotsList pots={pots}/>
                        :<h1>No pots yet</h1>}
                    </Row>
                    
                </Col>
            </Row>
        </Container>
      );
};


export default Home


