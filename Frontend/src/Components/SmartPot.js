import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';
import '../Styling/SmartPot.css'
import { getPotFromId } from '../Util/API_config';
import { useState, useEffect } from 'react';
import waterdropImage from '../images/waterdrop.png'
import { useAuth } from "../Util/AuthProvider";
import { useParams } from 'react-router-dom';
import WaterContainerChart from './WaterContainerChart';

const formatDate = (timestamp) => {
    const date = new Date(timestamp);
    const options = {
        month: 'short',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit'
    };
    return date.toLocaleDateString('en-US', options);
};

const SmartPot = ({ potID }) => {

    const [pot, setPot] = useState(null);
    const { setToken } = useAuth();
    const [latestMeasuredSoilData, setLatestMeasuredSoilData] = useState(null);
    const [warning, setWarning] = useState(false);

    const handlePotNotFound = () => {
        console.log("Pot not found error");
    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await getPotFromId(potID, setToken, handlePotNotFound);
                if (response && response.success) {
                    setPot(response.pot);
                    const sensorData = response.pot.sensorData;
                    setWarning(false);
                    if (sensorData && sensorData.length > 0) {
                        setLatestMeasuredSoilData(sensorData[sensorData.length - 1]);
                    } else {
                        setLatestMeasuredSoilData(null);
                    }
                } else {
                    console.error('Error fetching pot data:', response ? response.message : "Unknown error");
                }
            } catch (error) {
                console.error('Error fetching pot data:', error);
            }
        };
        if (potID) {
            fetchData();
        } else {
            console.error("potID is undefined");
        }
    }, [potID, setToken]);

    useEffect(() => {
        if (latestMeasuredSoilData && latestMeasuredSoilData.waterTankLevel < 25) {
            setWarning(true);
        } else {
            setWarning(false);
        }
    }, [latestMeasuredSoilData]);


    if (!pot) {
        return <div>Loading...</div>;
    }

    return (
        <Link to={"/pot-details/" + pot.id}>
            <Container id='smartPodContainer' data-testid='smartPodContainer' className={warning ? 'warningContainer' : ''}>
                <Row>
                    <Col id='bottomCol' md="8">
                        <h1>{pot.nameOfPot}</h1>
                        <h2>{"Plant: " + (pot.plant?.nameOfPlant ?? "No Plant")}</h2>
                        {latestMeasuredSoilData && <p>Soil Hydration:</p>}
                        <div className='waterDropContainer'>
                            <h1 id='percent'>{latestMeasuredSoilData ? latestMeasuredSoilData.measuredSoilMoisture + "%" : "No Data"}</h1>
                            {latestMeasuredSoilData && <img id='waterdrop' src={waterdropImage} />}
                        </div>
                        <p>WaterTank: {latestMeasuredSoilData ? latestMeasuredSoilData.waterTankLevel + "%" : "No Data"}</p>
                        <p>Last Watered: {latestMeasuredSoilData ? formatDate(latestMeasuredSoilData.timestamp) : "No Data"}</p>
                    </Col>
                    <Col md="4">
                        <img id='smartPotPlant' src={pot.plant?.image} onError={event => {
                            event.target.src = "https://img.freepik.com/premium-vector/home-plant-potted-plant-isolated-white-flat-vector-illustration_186332-890.jpg"
                            event.onerror = null
                        }} />
                    </Col>
                </Row>
            </Container>
        </Link>

    );
}



export default SmartPot

