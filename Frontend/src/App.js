import { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route, useParams, useHistory } from "react-router-dom";
import Home from './Pages/Home';
import Login from './Pages/Login';
import Register from './Pages/Register';
import ConnectPot from './Pages/ConnectPot';
import Overview from './Pages/PotDetails';
import PlantOverview from './Pages/PlantOverview.js';
import Navbar from './Components/NavBar';
import './Styling/Navbar.css';
import PotDetails from './Pages/PotDetails';
import'./Styling/App.css'
const App = () => {
  useEffect(() => {
    // Check if the authentication token exists in local storage
    const authToken = localStorage.getItem('token');
    console.log("Getting token: "+ authToken)

    // Get the current pathname
    const currentPath = window.location.pathname;

    // Routes that don't require authentication
    const publicRoutes = ['/register', '/login']; // Add more public routes if needed

    // If the token doesn't exist and the user is not trying to access a public route, redirect to the login page
    if (!authToken && !publicRoutes.includes(currentPath)) {
      window.location.href = '/login';
    }
  }, []);

    return (
        <div id='screen'>

            <BrowserRouter>
        <Navbar/>
        <Routes>
            <Route exact path="/" element={<Home />} />
            <Route exact path="/login" element={<Login />} />
            <Route exact path="/register" element={<Register />} />
            <Route exact path="/plant_overview" element={<PlantOverview />} />
            <Route exact path="/connect_pot" element={<ConnectPot />} />
            <Route path="/:potID" element={<PotDetails/>} />
        </Routes>
      </BrowserRouter>
   
        </div>
      )   
        
}

export default App;

