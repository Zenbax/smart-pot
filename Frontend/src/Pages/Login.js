import React, { useState } from 'react';
import {Link, Outlet, Route  } from 'react-router-dom';
import bcrypt from "bcryptjs";

import '../Styling/Login.css';
import { loginUser } from "../API/API_config";
import Register from './Register';


const Login =()=> {
    // State variables for email and password
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    // Function to handle form submission
    const handleSubmit = async(event) => {
        event.preventDefault();
        //console.log(password)
        //const hashedPassword = await bcrypt.hash(password,10)
        //console.log(hashedPassword)
        loginUser(email, password)
    };

    return (
        <div className="login-container">
            <h2 className="login-h2"> Login </h2>
            <form className="login-form" onSubmit={handleSubmit}>
                <div className="login-input-container">
                    <input
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>
                <div className="login-input-container">
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




