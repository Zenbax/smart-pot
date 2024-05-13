import React, { useState } from 'react';
import { Route } from 'react-router-dom';
import {Link} from 'react-router-dom';
import bcrypt from "bcryptjs";


import '../Styling/Login.css';
import { createUser, loginUser } from "../API/API_config";


const Register =()=> {
    // State variables for username and password
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState('')
    const [redirect, setRedirect] = useState(false);


    const handleClick = async(event)=> {
        event.preventDefault();
        handleCloseError();

        //Validering af bruger
        if (!name || !lastName || !password || !email || !phoneNumber ) {
           setError('All fields are required') 
           setShowError(true);
           return
        }
        if (!email.includes('@') || !email.includes('.')) {
            setError('Email is not valid');
            setShowError(true);
            return;
        }
        if(phoneNumber.length<8){
            setError('Phone Number is not valid');
            setShowError(true);
            return;
        }
        
        //const hashedPassword = await bcrypt.hash(password,10)
        createUser(name, lastName, password, email, phoneNumber);

        /*const loginSuccess = await loginUser(email, password)

        if (loginSuccess) {
            setRedirect(true);
        }*/

    }
    
    const handleCloseError = () => {
        setShowError(false);
        setError('');
    }


    return (
        <div className="login-container">
            <h2> Register </h2>
            <div className="box">
                <form className="login-form" onSubmit={handleClick}>
                    <div className="input-container">
                        <input
                            type="text"
                            placeholder="First Name"
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
                    {showError && (
                    <div className='error-popup'>
                    <p>{error}</p>
                    </div>
                )}
                    <button type='submit'> Submit</button>
                </form>
                
                {redirect && (
                <Route>
                    <Redirect to="/" />
                </Route>
            )}
            </div>
        </div>
    );
}

export default Register;


