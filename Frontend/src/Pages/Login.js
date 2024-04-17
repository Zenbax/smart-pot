import React, { useState } from 'react';
import {Link, Outlet, Route  } from 'react-router-dom';

import '../Styling/Login.css';
import { loginUser } from "../API/API_config";
import Register from './Register';


const Login =()=> {
    // State variables for username and password
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    // Function to handle form submission
    const handleSubmit = (event) => {
        event.preventDefault();
        // Here you can perform login logic using username and password
        // For example, sending a request to your server
    };

    return (
        <div className="login-container">
            <h2> Login </h2>
            <form className="login-form" onSubmit={handleSubmit}>
                <div className="input-container">
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </div>
                <div className="input-container">
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <button type="submit">Login</button>
                <Link to="/Register"><button>Register </button></Link>
                <Outlet />
            </form>
        </div>
    );
}

export default Login;




