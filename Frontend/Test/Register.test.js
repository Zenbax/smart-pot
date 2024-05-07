import React from 'react';
import {render, fireEvent, waitFor} from '@testing-library/react';
import '@testing-library/jest-dom';
import bcrypt from 'bcryptjs'
import Register from '../src/Pages/Register';
import { createUser } from './Mocks/API_config';

// TODO: Find ud af createUser

jest.mock("axios")

jest.mock('bcryptjs', () => ({
    hash: jest.fn((password, saltRounds) => Promise.resolve(`hashed-${password}`))
  }));

jest.mock('../src/API/API_config');

jest.setTimeout(15000);


test("Register rendes correctly", ()=>{
    const { getByText, getByPlaceholderText } = render(<Register />);
    
    expect(getByText('Register')).toBeInTheDocument();
    expect(getByPlaceholderText('First Name')).toBeInTheDocument();
    expect(getByPlaceholderText('Last Name')).toBeInTheDocument();
    expect(getByPlaceholderText('Password')).toBeInTheDocument();
    expect(getByPlaceholderText('Email')).toBeInTheDocument();
    expect(getByPlaceholderText('Phone Number')).toBeInTheDocument();
    expect(getByText('Submit')).toBeInTheDocument();
});




test("Registers user from register-page", async ()=>{
    const { getByPlaceholderText, getByText } = render(<Register />);
   
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'John' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Doe' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'john@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '1234567890' } });


    // Submit the form
    const submitButton = getByText('Submit');
    fireEvent.click(submitButton);

    //Note: createUser funger ikke 
    await waitFor(() => {
        console.log('createUser calls:', createUser.mock.calls);
        expect(bcrypt.hash).toHaveBeenCalledWith('password', 10);
        expect(createUser).toHaveBeenCalledWith('John', 'Doe', expect.any(String), 'john@example.com', '1234567890');
        expect(createUser).toHaveBeenCalledTimes(1);
    });

    
});


    



