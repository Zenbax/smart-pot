import React from "react";
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import PlantOverview from "../src/Pages/PlantOverview"
import { BrowserRouter, useNavigate } from "react-router-dom";

jest.mock('../src/Util/AuthProvider', () => ({
    useAuth: () => ({
        token: 'fake-token',
        setToken: jest.fn(),
    })
}));

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));

jest.mock('../src/Util/apiClient', () => ({
    getAllPlants: jest.fn().mockResolvedValue([
        { nameOfPlant: 'Fern', soilMinimumMoisture: 10, amountOfWaterToBeGiven: 25, image: "dGVzdA==", isDefault: true },
        { nameOfPlant: 'Jade Plant', soilMinimumMoisture: 20, amountOfWaterToBeGiven: 47, image: "dGVzdA==", isDefault: false }
    ])
}));

test('Render PlantOverView test', async () => {
    render(<PlantOverview />);

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

test('Search field test', async () => {
    render(<PlantOverview />);

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

test('Select plant template + displays data in Edit Plant component test', async () => {
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

    await waitFor(() => {
        expect(screen.getByText('Edit Plant')).toBeInTheDocument();
    });
});

test('Create + Delete plant popup test', async () => {
    render(
        <BrowserRouter>
            <PlantOverview />
        </BrowserRouter>
    );

    expect(screen.getByText('Create Plant')).toBeInTheDocument();

    // Wait for the plant templates to be loaded
    await waitFor(() => {
        expect(screen.getByText('Fern')).toBeInTheDocument();
        expect(screen.getByText('Jade Plant')).toBeInTheDocument();
    });

    // Click on the first plant template to select it
    fireEvent.click(screen.getByText('Fern'));

    await waitFor(() => {
        const plantNameInput = screen.getByPlaceholderText('Enter plant name');
        expect(plantNameInput).toHaveValue('Fern');
        expect(screen.getByText('Save Changes')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('Create New')).toBeInTheDocument();
        expect(screen.queryByText('Overwrite Existing')).not.toBeInTheDocument();
        expect(screen.getByText('Cancel')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Cancel'));

    await waitFor(() => {
        expect(screen.queryByText('Create New')).not.toBeInTheDocument();
        expect(screen.queryByText('Cancel')).not.toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Jade Plant'));

    await waitFor(() => {
        const plantNameInput = screen.getByPlaceholderText('Enter plant name');
        expect(plantNameInput).toHaveValue('Jade Plant');
        expect(screen.getByText('Save Changes')).toBeInTheDocument();
        expect(screen.getByText('Delete Plant')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('Create New')).toBeInTheDocument();
        expect(screen.getByText('Overwrite Existing')).toBeInTheDocument();
        expect(screen.getByText('Cancel')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Cancel'));

    await waitFor(() => {
        expect(screen.queryByText('Create New')).not.toBeInTheDocument();
        expect(screen.queryByText('Overwrite Existing')).not.toBeInTheDocument();
        expect(screen.queryByText('Cancel')).not.toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Delete Plant'));

    await waitFor(() => {
        expect(screen.getByText('You are about to delete: Jade Plant')).toBeInTheDocument();
        expect(screen.getByText('Delete')).toBeInTheDocument();
        expect(screen.getByText('Cancel')).toBeInTheDocument();
    });
});

test('Upload picture', async () => {
    render(
        <BrowserRouter>
            <PlantOverview />
        </BrowserRouter>
    );

    expect(screen.getByText('Create Plant')).toBeInTheDocument();

    // Find the file input
    const fileInput = screen.getByLabelText(/Upload a picture/i);

    // Create a mock file
    const file = new File(['dummy content'], 'example.png', { type: 'image/png' });

    // Simulate the file input change event
    fireEvent.change(fileInput, { target: { files: [file] } });

    // Check if the image preview is updated
    await waitFor(() => {
        const img = screen.getByAltText('Plant');
        expect(img).toHaveAttribute('src', expect.stringContaining('data:image/png;base64,ZHVtbXkgY29udGVudA=='));
    });
});

test('Error messages test', async () => {
    render(
        <BrowserRouter>
            <PlantOverview />
        </BrowserRouter>
    );

    expect(screen.getByText('Create Plant')).toBeInTheDocument();
    expect(screen.getByText('Save Changes')).toBeInTheDocument();

    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('All fields must be filled')).toBeInTheDocument();
    });

    const plantNameInput = screen.getByPlaceholderText('Enter plant name');
    const minMoistureInput = screen.getByPlaceholderText('Enter minimum moisture');
    const wateringAmountInput = screen.getByPlaceholderText('Enter watering amount');

    fireEvent.change(plantNameInput, { target: { value: 'TestName' } });
    fireEvent.change(minMoistureInput, { target: { value: 20 } });
    fireEvent.change(wateringAmountInput, { target: { value: 5 } });
    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('Plant must have an Image')).toBeInTheDocument();
    });

    const fileInput = screen.getByLabelText(/Upload a picture/i);

    // Create a mock file
    const file = new File(['dummy content'], 'example.png', { type: 'image/png' });

    // Simulate the file input change event
    fireEvent.change(fileInput, { target: { files: [file] } });

    // Check if the image preview is updated
    await waitFor(() => {
        const img = screen.getByAltText('Plant');
        expect(img).toHaveAttribute('src', expect.stringContaining('data:image/png;base64,ZHVtbXkgY29udGVudA=='));
    });
    
    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('Watering amount must be 20ml or higher')).toBeInTheDocument();
    });

    fireEvent.change(wateringAmountInput, { target: { value: 25 } });
    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
        expect(screen.getByText('Create New')).toBeInTheDocument();
        expect(screen.queryByText('Overwrite Existing')).not.toBeInTheDocument();
        expect(screen.getByText('Cancel')).toBeInTheDocument();
    });
});


test('Back button goes back', async () => {
    const navigateMock = jest.fn();
    useNavigate.mockReturnValue(navigateMock);
    const historyBackMock = jest.fn();
    global.window.history.back = historyBackMock;

    render(
        <BrowserRouter>
            <PlantOverview />
        </BrowserRouter>
    );

    fireEvent.click(screen.getByText('Back'));

    await waitFor(() => {
        expect(historyBackMock).toHaveBeenCalled();
    });
});

