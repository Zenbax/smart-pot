import { useState } from 'react';
import { BrowserRouter, Routes, Route, useParams } from "react-router-dom";
import Home from './Pages/Home';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Connect from './Pages/Connect';
import Overview from './Pages/PotDetails';
import Navbar from './Components/NavBar';
import './Styling/Navbar.css';
import PotDetails from './Pages/PotDetails';



function App (){
    return (
        
        <BrowserRouter>
        <Navbar/>
        <Routes>
            <Route exact path="/" element={<Home />} />
            <Route exact path="/login" element={<Login />} />
            <Route exact path="/register" element={<Register />} />
            <Route exact path="/connect" component={<Connect />} />
            <Route path="/:potID" element={<PotDetails/>} />
        </Routes>
      </BrowserRouter>
    )
}

export default App;

