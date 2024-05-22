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
        expect(screen.getByTestId('plant-template-0')).toBeInTheDocument();
        expect(screen.getByTestId('plant-template-1')).toBeInTheDocument();
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
