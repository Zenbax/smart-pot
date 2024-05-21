import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import '../Styling/Navbar.css';
import { useAuth } from '../Util/AuthProvider';

const Navbar = () => {
  const { setToken } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogoutClick = () => {
    console.log('Logout button clicked!');
    setToken("");
    navigate("/Login");
  };

  const homeClick = () => {
    navigate("/");
    window.location.reload();
  };

  const isLoginPage = location.pathname === "/Login";

  return (
    <nav className="nav-container fixed-top">
      <div className="logo" onClick={homeClick} style={{ cursor: 'pointer' }}>
        Smart-Pot
      </div>
      {!isLoginPage && (
        <button type="button" className="btn btn-danger" onClick={handleLogoutClick}>
          Logout
        </button>
      )}
    </nav>
  );
};

export default Navbar;
