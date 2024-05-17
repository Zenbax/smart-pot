import React from 'react';
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
  


const homeClick = () => {
  to = "/"
}

   return (
      <nav className="nav-container">
        <Link onClick={homeClick}><div className="logo">Smart-Pot</div></Link>
        <Link to="/login"><button type="button" className="login-button" onClick={handleClick}> Logout</button></Link>
      </nav>
    );


}

export default Navbar



