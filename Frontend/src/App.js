import { useState, useEffect } from 'react';
import { BrowserRouter, Route, useParams, useHistory } from "react-router-dom";
import Routes from "./Util/routes.js";
import Home from './Pages/Home';
import Login from './Pages/Login';
import Register from './Pages/Register';
import ConnectPot from './Pages/ConnectPot';
import PlantOverview from './Pages/PlantOverview.js';
import Navbar from './Components/NavBar';
import './Styling/Navbar.css';
import PotDetails from './Pages/PotDetails';
import'./Styling/App.css'
import AuthProvider from './Util/AuthProvider.js';
const App = () => {
 
    return (
        <div id='screen'>
            

<AuthProvider>
      <BrowserRouter>
            <Navbar/>
            </BrowserRouter>

        <Routes/>
  </AuthProvider>

      
   
        </div>
      )   
        
}

export default App;

