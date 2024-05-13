import { useState } from 'react';
import { BrowserRouter, Routes, Route, useParams } from "react-router-dom";
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



function App (){
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

