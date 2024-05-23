import React from "react";
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import PlantOverview from "../src/Pages/PlantOverview"
import { BrowserRouter } from "react-router-dom";

jest.mock('../src/Util/AuthProvider', () => ({
    useAuth: () => ({
        token: 'fake-token',
        setToken: jest.fn(),
    })
}));

jest.mock('../src/Util/API_config', () => ({
    getAllPlants: jest.fn().mockResolvedValue([
        { nameOfPlant: 'Fern', soilMinimumMoisture: 10, amountOfWaterToBeGiven: 25, image: null, isDefault: true },
        { nameOfPlant: 'Jade Plant', soilMinimumMoisture: 20, amountOfWaterToBeGiven: 47, image: null, isDefault: false }
    ])
}));

test('Render PlantOverView correctly', async () => {
    render( <PlantOverview />);

    expect(screen.getByText('Back')).toBeInTheDocument();
    expect(screen.getByText('Plants')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('search for plant')).toBeInTheDocument();

    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.getByText('Jade Plant')).toBeInTheDocument();
    });

    expect(screen.getByText('Create Plant')).toBeInTheDocument();
    expect(screen.getByText('Plant Name:')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter plant name")).toBeInTheDocument();
    expect(screen.getByText('Minimum Soil Moisture:')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter minimum moisture")).toBeInTheDocument();
    expect(screen.getByText('Watering Amount (ml):')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter watering amount")).toBeInTheDocument();
    expect(screen.getByText('Upload a picture')).toBeInTheDocument();
    expect(screen.getByText('Save Changes')).toBeInTheDocument();
    expect(screen.getByAltText('Plant')).toBeInTheDocument();
});

test('Search field works correctly', async () => {
    render( <PlantOverview /> );

    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.getByText('Jade Plant')).toBeInTheDocument();
    });

    const searchInput = screen.getByPlaceholderText('search for plant');
    fireEvent.change(searchInput, { target: { value: 'fern' } });

    // Wait for the search results to be filtered
    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.queryByText('Jade Plant')).not.toBeInTheDocument();
    });

    // Ensure that the search input value has updated before resetting it
    await waitFor(() => {
        expect(searchInput).toHaveValue('fern');
    });

    // Reset the search input
    fireEvent.change(searchInput, { target: { value: '' } });

    // Wait for the search results to be reset
    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.getByText('Jade Plant')).toBeInTheDocument();
    });
});

test('Select plant template displays data in Edit Plant component', async () => {
    render(
        <BrowserRouter>
            <PlantOverview />
        </BrowserRouter>
    );

    // Wait for the plant templates to be loaded
    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.getByText('Jade Plant')).toBeInTheDocument();
        expect(screen.getByText('Create Plant')).toBeInTheDocument();
    });

    // Click on the first plant template to select it
    fireEvent.click(screen.getByText('Fern'));

    // Log the input values for debugging
    await waitFor(() => {
        const plantNameInput = screen.getByPlaceholderText('Enter plant name');
        const minMoistureInput = screen.getByPlaceholderText('Enter minimum moisture');
        const wateringAmountInput = screen.getByPlaceholderText('Enter watering amount');
        
        console.log("Plant name input value:", plantNameInput.value);
        console.log("Minimum moisture input value:", minMoistureInput.value);
        console.log("Watering amount input value:", wateringAmountInput.value);

        expect(plantNameInput).toHaveValue('Fern');
        expect(minMoistureInput).toHaveValue(10);
        expect(wateringAmountInput).toHaveValue(25);
    });

    // Ensure that the Edit Plant component is fully rendered
    await waitFor(() => {
        expect(screen.getByText('Edit Plant')).toBeInTheDocument();
    });
});
