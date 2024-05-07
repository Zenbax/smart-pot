import React from 'react';
import {render, screen, fireEvent} from '@testing-library/react';
import '@testing-library/jest-dom';
import { MemoryRouter, BrowserRouter as Router } from 'react-router-dom';
import Navbar from '../src/Components/NavBar'


test("Navbar is rendered on page", () =>{
    render(
        <MemoryRouter>
            <Navbar />
        </MemoryRouter>
    );
    expect(screen.getByText('Smart-Pot')).toBeInTheDocument();
    expect(screen.getByText('Login')).toBeInTheDocument();
});





test("Click on button on the Navbar and navigate to Login-page",() => {
    const {getByText}  =render(
        <Router>
            <Navbar/>
        </Router>
    )

    fireEvent.click(getByText('Login'));

    expect(window.location.pathname).toBe('/login');
});


test("Navigate to Homepage from Navbar", () =>{
    const {getByText}  =render(
        <Router>
            <Navbar/>
        </Router>
    )

    fireEvent.click(getByText('Smart-Pot'));

    expect(window.location.pathname).toBe('/');

});



