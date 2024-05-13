import React, {useState, useEffect} from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
import {Container, Row, Col} from 'react-bootstrap';
import PotsList from "../Components/PotsList";
import { getAllPots } from "../API/API_config";
import '../Styling/Home.css'
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
        <Container className="bitchContainer">
            <Row className="flex-column h-100">
                <Col md="4">
                    <Row>
                        <div>Profile</div>
                    </Row>
                    <Row>
                         <Link to="/connect">
                            <button type="button" class="btn btn-outline-dark">Forbind potte</button>
                        </Link>
                        <Link to="/plant_overview">
                            <button type="button" class="btn btn-outline-dark">Oversigt</button>
                        </Link>
                    </Row>

                </Col>
                <Col md="8" className="flex-grow-1 h-100">
                    <Row>
                        <h1>Potter</h1>
                    </Row>
                    <Row className=" flex-grow-1 problemRow" >
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


