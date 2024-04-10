import React, { useState } from 'react';
import { Route } from 'react-router-dom';
import {Link} from 'react-router-dom';

import '../Styling_Pages/Login.css';
import { createUser } from "../API/API_config";


const Register =()=> {
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
            <h2> Register </h2>
            <div className="box">
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
                    <button type="submit">Register</button>
                  
                </form>
            </div>
        </div>
    );
}

export default Register;


