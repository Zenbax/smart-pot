import { useState } from 'react';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './Pages/Home';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Connect from './Pages/Connect';
import Navbar from "./Components/NavBar";



function App (){
    return (
        
        <BrowserRouter>
        <Navbar/>
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register/>}/>
            <Route path="/connect" element={<connect/>}/>
        </Routes>
      </BrowserRouter>
    )
}

export default App;

