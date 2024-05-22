import React, { useState, useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';
import { Container, Row, Col } from 'react-bootstrap';
import PotsList from "../Components/PotsList";
import { getAllPots } from "../Util/API_config";
import '../Styling/Home.css'
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

    }, [token])

    return (
        <Container className="homeContainer">
            <Row className="flex-column h-100">
                <Col md="4">
                    <Row>
                        <div className="container mt-4">
                            <div className="row justify-content">
                                <div className="col">
                                    <div className="card">
                                        <div className="card-header text-center text-white">
                                            Profile
                                        </div>
                                        <div className="card-body">
                                            <p className="card-text"><strong>Name:</strong> {userData.name} {userData.lastName}</p>
                                            <p className="card-text"><strong>Email:</strong> {userData.email}</p>
                                            <p className="card-text"><strong>Phone:</strong> {userData.phoneNumber}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                            <PotsList pots={pots} />
                            : <h1>No pots yet</h1>}
                    </Row>

                </Col>
            </Row>
        </Container>
    );
};


export default Home


