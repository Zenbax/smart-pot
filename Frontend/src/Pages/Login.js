import React, { useState } from 'react';
import {Link, Outlet, Route  , useNavigate} from 'react-router-dom';
import { MD5 } from 'crypto-js';
import { useAuth } from '../Util/AuthProvider';

import '../Styling/Login.css';
import { loginUser } from "../Util/API_config";
import Register from './Register';


const Login =()=> {
    // State variables for email and password
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { setToken } = useAuth();

    const navigate = useNavigate();

    // Function to handle form submission
    const handleSubmit = async(event) => {
        event.preventDefault();
        const hashedPassword = MD5(password).toString();
        if (await loginUser(email, hashedPassword, setToken)){
            navigate('/');
        }
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