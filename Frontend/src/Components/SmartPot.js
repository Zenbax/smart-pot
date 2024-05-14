import React from 'react';
import {Container, Row, Col} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';
import '../Styling/SmartPot.css'
import waterdropImage from '../images/waterdrop.jpg'


const SmartPot = ({pot}) => {
    return(
        <Link to={"/"+pot.id}>
        <Container id='smartPodContainer'>
            <Row>
                <Col id='bottomCol' md="8">
                    <h1>{pot.nameOfPot}</h1>
                    <h2>{"Plant: "+pot.plant?.nameOfPlant}</h2>
                    <p>Soil Hydration:</p>
                    <h1 id='percent'>{55/*hent fra nyeste humidity log*/ +"%"}</h1>
                    <img id='waterdrop' src={waterdropImage}/>
                </Col>
                <Col md="4">
                    <img id='smartPotPlant' src={pot.plant?.imageUrl} onError={event => {
                        event.target.src = "https://img.freepik.com/premium-vector/home-plant-potted-plant-isolated-white-flat-vector-illustration_186332-890.jpg"
                        event.onerror = null
                        }}/>
                </Col>
            </Row>
        </Container>
        </Link>
        
    );
}


export default SmartPot


