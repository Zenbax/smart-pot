import { useState } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './Pages/Home';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Connect from './Pages/Connect';
import Overview from './Pages/Overview';
import Navbar from './Components/NavBar';
import './Styling/Navbar.css';



function App (){
    return (
        
        <BrowserRouter>
        <Navbar/>
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/connect" component={<Connect />} />
            <Route path="/overview" component={<Overview />} />
        </Routes>
      </BrowserRouter>
    )
}

export default App;

