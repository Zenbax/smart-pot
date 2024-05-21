import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Link, Route  } from 'react-router-dom';
import '../Styling/Navbar.css';
import { useAuth } from '../Util/AuthProvider';
import { useNavigate } from 'react-router-dom';

const Navbar=()=> {
  const { setToken } = useAuth();
  const handleClick = () => {
    console.log('Button clicked!');
    setToken("");
  };
  
const navigate = useNavigate();

const homeClick = () => {
  navigate("/") 
  to = "/"
}


const isLoginPage = location.pathname === "/Login";

   return (
      <nav className="nav-container">
        <Link onClick={homeClick}><div className="logo">Smart-Pot</div></Link> 
        {!isLoginPage && (
        <Link to="/Login">
          <button type="button" class="btn btn-danger" onClick={handleClick}>Logout</button>
        </Link>)}
      </nav>
    );


}

export default Navbar



