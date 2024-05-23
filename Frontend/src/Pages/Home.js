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
        <Container fluid className="homeContainer">
            <Row>
                <Col md={4} className="profileColumn">
                    <div className="card mt-4">
                        <div className="card-header text-center text-white">
                            Profile
                        </div>
                        <div className="card-body">
                            <p className="card-text"><strong>Name:</strong> {userData.name} {userData.lastName}</p>
                            <p className="card-text"><strong>Email:</strong> {userData.email}</p>
                            <p className="card-text"><strong>Phone:</strong> {userData.phoneNumber}</p>
                        </div>
                    </div>
                    <div className="d-grid">
                        <Link to="/connect_pot" className="btn btn-outline-dark mt-3">Connect pot</Link>
                        <Link to="/plant_overview" className="btn btn-outline-dark mt-3">Plant Overview</Link>
                    </div>
                </Col>
                <Col md={8}>
                    <Row>
                        <Col>
                            <h1 className="mt-3 mt-md-0">Pots</h1>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            {pots && pots.length ? <PotsList pots={pots} /> : <h1>No pots yet</h1>}
                        </Col>
                    </Row>
                </Col>
            </Row>
        </Container>
    );
};

export default Home;
