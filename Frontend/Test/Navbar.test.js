import React from 'react';
import {render, screen, fireEvent} from '@testing-library/react';
import '@testing-library/jest-dom';
import { MemoryRouter, BrowserRouter as Router } from 'react-router-dom';
import Navbar from '../src/Components/NavBar'
import { useNavigate, useLocation } from 'react-router-dom';

jest.mock('../src/Util/AuthProvider', () => ({
    useAuth: () => ({
      setToken: jest.fn()
    })
  }));

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useLocation: jest.fn()
  }));

  beforeEach(() => {
    jest.clearAllMocks();
  });



test("Navbar is rendered on Login-page", () =>{
    useLocation.mockReturnValue({ pathname: '/Login' });
    render(
        <MemoryRouter>
            <Navbar />
        </MemoryRouter>
    );
    expect(screen.getByText('Smart-Pot')).toBeInTheDocument();    
});


test("Navbar is rendered on Home-page", () =>{
    useLocation.mockReturnValue({ pathname: '/Home' });
    render(
        <MemoryRouter>
            <Navbar />
        </MemoryRouter>
    );
    expect(screen.getByText('Smart-Pot')).toBeInTheDocument();
    expect(screen.getByText("Logout")).toBeInTheDocument();
});

test("Click on button on the Navbar and navigate to Login-page",() => {


    useLocation.mockReturnValue({ pathname: '/' });
    const {getByText} =render(
        <Router>
            <Navbar/>
        </Router>
    )

    fireEvent.click(screen.getByRole('button', { name: 'Logout' }));
    expect(window.location.pathname).toBe('/Login');

});


test("Navigate to Homepage from Navbar", () =>{
    useLocation.mockReturnValue({ pathname: '/ConnectPot' });
    const {getByText}  =render(
        <Router>
            <Navbar/>
        </Router>
    )

    fireEvent.click(getByText('Smart-Pot'));

    expect(window.location.pathname).toBe('/');


});



