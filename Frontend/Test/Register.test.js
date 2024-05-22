
import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import { useNavigate } from 'react-router-dom';
import Register from '../src/Pages/Register';
import { createUser } from '../src/Util/API_config';
import { MD5 } from 'crypto-js';


jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(), 
  }));

jest.mock('crypto-js', () => ({
    MD5: jest.fn().mockReturnValue({
      toString: () => 'hashedPassword'
    })
  }));

  jest.mock('../src/Util/API_config', () => ({
    createUser: jest.fn(),
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



test('Submits form correctly', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    createUser.mockResolvedValueOnce(true);

    const { getByText, getByPlaceholderText } = render(<Register />);

    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(MD5).toHaveBeenCalledWith('password');

    await waitFor (() => {
        expect(navigateMock).toHaveBeenCalledWith('/login');
    });
});

test('Submits form with empty fields', async () => {

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

test('Submits form email not valid', async () => {

    const navigateMock = jest.fn();
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);
  
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte$example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(getByText("Email is not valid")).toBeInTheDocument();
});

test('Submits form Phone number not valid', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    const { getByText, getByPlaceholderText } = render(<Register />);

    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '15678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));

    expect(getByText("Phone Number is not valid")).toBeInTheDocument();
});


test('Submits form User already exist', async () => {

    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
    createUser.mockRejectedValueOnce({ response: { data: { message: "Email already exists." }}});

    const { getByText, getByPlaceholderText } = render(<Register />);
  
    fireEvent.change(getByPlaceholderText('First Name'), { target: { value: 'Ditte' } });
    fireEvent.change(getByPlaceholderText('Last Name'), { target: { value: 'Hej' } });
    fireEvent.change(getByPlaceholderText('Email'), { target: { value: 'Ditte@example.com' } });
    fireEvent.change(getByPlaceholderText('Phone Number'), { target: { value: '12345678' } });
    fireEvent.change(getByPlaceholderText('Password'), { target: { value: 'password' } });

    fireEvent.click(getByText('Submit'));
    
    await waitFor(() => {
        expect(getByText("Email already exists.")).toBeInTheDocument();
    });
});


test('Click button Back to Login page', async () => {
    const navigateMock = jest.fn();
    useNavigate.mockReturnValue(navigateMock);
    const { getByText} = render(<Register />);
    
    fireEvent.click(getByText('Back'));

    await waitFor (() => {
        expect(navigateMock).toHaveBeenCalledWith('/login');
    });
});
  
    
