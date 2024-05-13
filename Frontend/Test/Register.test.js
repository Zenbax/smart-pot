
import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import { useNavigate } from 'react-router-dom';
import Register from '../src/Pages/Register';
import { createUser } from '../src/API/API_config';


jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(), 
  }));


test("Register rendes correctly", () => {
    const { getByText, getByPlaceholderText } = render(<Register />);
  
    expect(getByText('Register')).toBeInTheDocument();

    expect(getByPlaceholderText('First Name')).toBeInTheDocument();
    expect(getByPlaceholderText('Last Name')).toBeInTheDocument();
    expect(getByPlaceholderText('Email')).toBeInTheDocument();
    expect(getByPlaceholderText('Phone Number')).toBeInTheDocument();
    expect(getByPlaceholderText('Password')).toBeInTheDocument();

    expect(getByText('Back')).toBeInTheDocument();
    expect(getByText('Submit')).toBeInTheDocument();
});



test('submits form correctly', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);

    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(navigateMock).toHaveBeenCalledWith('/login');
});

test('submits form with empty fields', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);
  
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: '' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: '' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: '' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: '' } });

    fireEvent.click(getByText('Submit'));

    expect(getByText("All fields are required")).toBeInTheDocument();
});

test('submits form email not valid', async () => {

    const navigateMock = jest.fn();
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);
  
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte$example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(getByText("Email is not valid")).toBeInTheDocument;
});

test('submits form Phone number not valid', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);

    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '15678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(getByText("Phone Number is not valid")).toBeInTheDocument;
});

//Todo: 
/*
test('submits form User already exist', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);
  
    createUser("Jane", "Hej","password", "Ditte@example.com","12345678")
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    //Skal skrive hvad der kommer til at ske her
    expect(getByText(""));
});
*/

test('Click button Back to Login page', async () => {
    const navigateMock = jest.fn();
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);
    
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'John' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Doe' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'john.doe@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });
  
    fireEvent.click(getByText('Back'));

    expect(navigateMock).toHaveBeenCalledWith('/login');
});
  
    
