import React from "react";
import '@testing-library/jest-dom'
import { render, fireEvent, screen, waitFor, getByText, getByLabelText, getByRole, getByTitle } from '@testing-library/react';
import Home from "../src/Pages/Home";
import SmartPot from "../src/Components/SmartPot";
import { BrowserRouter } from "react-router-dom";


jest.mock("../src/API/API_config", () => ({
    getAllPots: jest.fn().mockResolvedValue([
        { nameOfPot: "Pot_1", email: "kalle@da.ag", machineId: "123", enable: true, nameOfPlant: 'Lilje' },
        { nameOfPot: "Pot_2", email: "kalle@da.ag", machineId: "456", enable: false, nameOfPlant: null },
        // Add more mock data as needed
    ]),
}));

  test('Home-page renders correctly', async () => {  
    render(
        <BrowserRouter>
            <Home />
        </BrowserRouter>
    );

    //Profil
    expect(screen.getByText('Profile')).toBeInTheDocument();

    const connectPotButton = screen.getByText('Connect pot');
    const overviewButton = screen.getByText('Overview');
    expect(connectPotButton).toBeInTheDocument();
    expect(overviewButton).toBeInTheDocument();

    expect(connectPotButton.closest('a')).toHaveAttribute('href', '/connect_pot');
    expect(overviewButton.closest('a')).toHaveAttribute('href', '/plant_overview');


    await waitFor(() => expect(require('../src/API/API_config').getAllPots).toHaveBeenCalled());

    //SmartPot component 
    const potContainers = screen.getAllByTestId("smartPodContainer");
    expect(potContainers.length).toBeGreaterThan(0); 

    expect(potContainers[0]).toHaveTextContent('Pot_1');
    expect(potContainers[1]).toHaveTextContent('Pot_2');
});


test("renders 'No pots yet' message when no pots are available", async () => {
    require('../src/API/API_config').getAllPots.mockResolvedValueOnce([]);
    render(
        <BrowserRouter>
            <Home />
        </BrowserRouter>
    );

    await waitFor(() => expect(require('../src/API/API_config').getAllPots).toHaveBeenCalled());

    expect(screen.queryByText("No pots yet")).toBeInTheDocument();
});



