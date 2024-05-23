import React, { useState, useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';
import { Container, Row, Col } from 'react-bootstrap';
import PotsList from "../Components/PotsList";
import { getAllPots } from "../Util/API_config";
import '../Styling/Home.css';
import { useAuth } from "../Util/AuthProvider";

const Home = () => {
    const { token, setToken } = useAuth();
    const [pots, setPots] = useState([]);
    const userData = JSON.parse(localStorage.getItem('user'));

    useEffect(() => {
        if (token) {
            const fetchData = async () => {
                const potsData = await getAllPots(setToken);
                setPots(potsData);
            };
            fetchData();
        }
    }, [token]);

    return (
        <Container className="homeContainer">
            <Row className="flex-column flex-md-row h-100">
                <Col md="4" className="mb-3 mb-md-0">
                    <div className="card profileCard">
                        <div className="card-header text-center text-white">
                            Profile
                        </div>
                        <div className="card-body">
                            <p className="card-text"><strong>Name:</strong> {userData.name} {userData.lastName}</p>
                            <p className="card-text"><strong>Email:</strong> {userData.email}</p>
                            <p className="card-text"><strong>Phone:</strong> {userData.phoneNumber}</p>
                        </div>
                    </div>
                    <div className="d-flex flex-column">
                        <button type="button" className="btn btn-outline-dark mb-2">
                            <Link to="/connect_pot">Connect pot</Link>
                        </button>
                        <button type="button" className="btn btn-outline-dark">
                            <Link to="/plant_overview">Plant Overview</Link>
                        </button>
                    </div>
                </Col>
                <Col md="8" className="flex-grow-1 h-100">
                    <Row>
                        <h1>Pots</h1>
                    </Row>
                    <Row className="flex-grow-1 homeRow">
                        {pots && pots.length ? 
                            <PotsList pots={pots} /> 
                            : <h1>No pots yet</h1>}
                    </Row>
                </Col>
            </Row>
        </Container>
    );
};

export default Home;
