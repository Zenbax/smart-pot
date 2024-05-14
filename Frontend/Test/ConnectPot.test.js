import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom';
import ConnectPot from '../src/Pages/ConnectPot';
import { BrowserRouter } from 'react-router-dom';

test('Render ConnectPot correctly', () => { 
    render(<ConnectPot/>);

    expect(screen.getByText('Connect your Smart-pot')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Smart-pot ID')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Name your pot')).toBeInTheDocument();
});

test('Typing into input fields', () => { 
    render(<ConnectPot/>);

    const idInput = screen.getByPlaceholderText('Smart-pot ID');
    const nameInput = screen.getByPlaceholderText('Name your pot');

    fireEvent.change(idInput, { target: { value: '123456' } });
    fireEvent.change(nameInput, { target: { value: 'My Smart Pot' } });

    expect(idInput).toHaveValue('123456');
    expect(nameInput).toHaveValue('My Smart Pot');
});

test('Pop-up when clicking add plant and close when cancel', () => {
    render(
        <BrowserRouter>
          <ConnectPot />
        </BrowserRouter>
      );

    const addButton = screen.getByText('Add a plant');
    fireEvent.click(addButton);

    expect(screen.getByText('Plants')).toBeInTheDocument();
    
    const cancelButton = screen.getByText('Cancel');
    fireEvent.click(cancelButton);

    expect(screen.queryByText('Plants')).not.toBeInTheDocument();
});

test('Selecting a plant template in the pop-up', () => {
    render(
        <BrowserRouter>
          <ConnectPot />
        </BrowserRouter>
      );

    const addButton = screen.getByText('Add a plant');
    fireEvent.click(addButton);

    const plantTemplate = screen.getAllByTestId('plant-template')[0]; // added a TestId to PlantTemp
    fireEvent.click(plantTemplate);

    expect(screen.queryByText('Plants')).not.toBeInTheDocument();
});
