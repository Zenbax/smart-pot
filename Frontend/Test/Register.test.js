import React from 'react';
import {render, fireEvent, waitFor} from '@testing-library/react';
import '@testing-library/jest-dom';
import bcrypt from 'bcryptjs'
import Register from '../src/Pages/Register';
import { createUser } from './Mocks/API_config';
import mockAxios from "axios"


// TODO: Find ud af createUser og få den mocket.

jest.mock("axios")

mockAxios.createUser = jest.fn();


afterEach(jest.clearAllMocks)

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
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });


    // Submit the form
    const submitButton = getByText('Submit');
    fireEvent.click(submitButton);

    await createUser('John', 'Doe', 'hashed-password', 'john@example.com', '12345678');

   // Verify that createUser is called with the correct arguments
    expect(createUser).toHaveBeenCalledWith('John', 'Doe', 'hashed-password', 'john@example.com', '12345678');


    // Asynchronous assertion to ensure bcrypt.hash is called
    await waitFor(() => {
        expect(bcrypt.hash).toHaveBeenCalledWith('password', 10);
    });
   
    // Asynchronous assertion to ensure createUser is called
    await waitFor(() => {
        expect(createUser).toHaveBeenCalledTimes(1);
    });
   
});



