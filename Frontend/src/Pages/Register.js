import React, { useState } from 'react';
import { Route } from 'react-router-dom';
import {Link} from 'react-router-dom';
import bcrypt from "bcryptjs";


import '../Styling/Login.css';
import { createUser } from "../API/API_config";


const Register =()=> {
    // State variables for username and password
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    

    const handleClick = async(event)=> {
        event.preventDefault();
        const hashedPassword = await bcrypt.hash(password,10)
        createUser(name, lastName, hashedPassword, email, phoneNumber);
    }

    return (
        <div className="login-container">
            <h2> Register </h2>
            <div className="box">
                <form className="login-form" onSubmit={handleClick}>
                    <div className="input-container">
                        <input
                            type="text"
                            placeholder="name"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    </div>
                    <div className="input-container">
                        <input
                            type="text"
                            placeholder="Last Name"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
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
                    <div className="input-container">
                        <input
                            type="text"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div className="input-container">
                        <input
                            type="text"
                            placeholder="Phone Number"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                        />
                    </div>
                    <button type='submit' >Register</button>
                  
                </form>
            </div>
        </div>
    );
}

export default Register;


