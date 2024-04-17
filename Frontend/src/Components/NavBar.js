import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
import '../Styling/Navbar.css';

const Navbar=()=> {
  
   return (
      <nav className="nav-container">
        <Link to="/"><div className="logo">Smart-Pot</div></Link>
        <Link to="/login"><button type="button" className="login-button"> Login</button></Link>
      </nav>
    );


}

export default Navbar



