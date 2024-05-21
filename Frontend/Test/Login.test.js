import React from 'react';
import { render, fireEvent, screen, waitFor } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import Login from '../src/Pages/Login';
import { loginUser } from '../src/Util/API_config';
import '@testing-library/jest-dom';
import { useNavigate } from 'react-router-dom';
import { MD5 } from 'crypto-js';
import { useAuth } from '../src/Util/AuthProvider';


  jest.mock('../src/Util/AuthProvider', () => ({
  useAuth: () => ({
    setToken: jest.fn() 
    })
  }));

  jest.mock("../src/Util/API_config", () => ({
    loginUser: jest.fn()
  }));
  
  jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(), 
  }));
  
  jest.mock('crypto-js', () => ({
    MD5: jest.fn().mockReturnValue({
      toString: () => 'hashedPassword'
    })
  }));



test("renders the login form with all fields and buttons", () => {
    render(
        <BrowserRouter>
          <Login />
        </BrowserRouter>
    );
  
    expect(screen.getAllByRole('heading', {name: 'Login'}))
    expect(screen.getByPlaceholderText("Email")).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Password")).toBeInTheDocument();
    expect(screen.getByRole('button', {name: 'Login'})).toBeInTheDocument();
    expect(screen.getByRole('link', { name: 'Register' })).toBeInTheDocument();
});
  


test("submits the form and calls navigate on successful login", async () => {
    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
  
    loginUser.mockImplementation((email, password) => true);  // Mock loginUser to return true
  
    render(
        <BrowserRouter>
          <Login />
        </BrowserRouter>
    );
  
    fireEvent.change(screen.getByPlaceholderText("Email"), { target: { value: "test@example.com" } });
    fireEvent.change(screen.getByPlaceholderText("Password"), { target: { value: "password" } });

    fireEvent.click(screen.getByRole('button',{name: 'Login'}));

    expect(MD5).toHaveBeenCalledWith('password');
      
    await waitFor (() => {
      expect(navigateMock).toHaveBeenCalledWith('/');
    })
   

});
  

test("submits the form with an unsuccessful login", async () => {
    const navigateMock = jest.fn(); 
    useNavigate.mockReturnValue(navigateMock);
  
    loginUser.mockImplementation((email, password) => false); 
  
    render(
        <BrowserRouter>
          <Login />
        </BrowserRouter>
    );
  
    fireEvent.change(screen.getByPlaceholderText("Email"), { target: { value: "test@example.com" } });
    fireEvent.change(screen.getByPlaceholderText("Password"), { target: { value: "password" } });

    fireEvent.click(screen.getByRole('button',{name: 'Login'}));

    expect(MD5).toHaveBeenCalledWith('password');
      
    await waitFor (() => {
      expect(navigateMock).not.toHaveBeenCalled();
    });

});


