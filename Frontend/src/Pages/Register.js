import React, { useState } from 'react';
import { MD5 } from 'crypto-js';

import { useNavigate } from 'react-router-dom';



import '../Styling/Login.css';
import { createUser } from "../Util/apiClient";


const Register =()=> {
    // State variables for username and password
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [showError, setShowError] = useState(false);
    const [error, setError] = useState('')


    const navigate = useNavigate();

    const handleClick = async(event)=> {
        event.preventDefault();
        handleCloseError();

        //Validering af bruger
        if (!name || !lastName || !password || !email || !phoneNumber) {
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
        if(phoneNumber.length<8){
            setError('Password must be 8 characters long');
            setShowError(true);
            return;
        }
        
        const hashedPassword = MD5(password).toString();
        try {
            const response = await createUser(name, lastName, hashedPassword, email, phoneNumber);
            if (response === true) {
                navigate('/login');
            } else {
                setError(response);
                setShowError(true);
            }
        } catch (e) {
            setError(e.response.data.message);
            setShowError(true);
        }
    }
    
    const handleCloseError = () => {
        setShowError(false);
        setError('');
    }

    const handleClickBack = ()  => {
        navigate('/login');
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
                    <div className="input-container">
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>

                    {showError && (
                    <div className='error-popup'>
                    <p>{error}</p>
                    </div>
                )}
                    <button type='back' class="btn btn-secondary" onClick={handleClickBack}>Back</button>
                    <button type='submit' class="btn btn-success" onClick={handleClick}> Submit</button>
                </form>
                
            </div>
        </div>
    );
}

export default Register;


