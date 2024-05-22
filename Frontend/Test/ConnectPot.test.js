import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import ConnectPot from '../src/Pages/ConnectPot';
import { BrowserRouter, useNavigate } from 'react-router-dom';
import { getAllPlants, createPot } from '../src/Util/API_config';

jest.mock('../src/Util/API_config', () => ({
  getAllPlants: jest.fn(),
  createPot: jest.fn(),
}));


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

const mockPlants = [
  {
      nameOfPlant: 'Rose',
      soilMinimumMoisture: 20,
      amountOfWaterToBeGiven: 100,
      image: 'rose.png'
  },
  {
      nameOfPlant: 'Tulip',
      soilMinimumMoisture: 15,
      amountOfWaterToBeGiven: 80,
      image: 'tulip.png'
  }
];

test('Render ConnectPot correctly', () => { 
    render(<ConnectPot/>);

    expect(screen.getByText('Connect your Smart-pot')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Smart-pot ID')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Name your pot')).toBeInTheDocument();
    expect(screen.getByText("Add a plant")).toBeInTheDocument();
    expect(screen.getByText("Connect Smart-pot")).toBeInTheDocument();
});

test('Connect a pot', async () => {
  const navigateMock = jest.fn(); 
  useNavigate.mockReturnValue(navigateMock);
  createPot.mockResolvedValueOnce(true);

  render(
    <BrowserRouter>
      <ConnectPot />
    </BrowserRouter>
  );

  const idInput = screen.getByPlaceholderText('Smart-pot ID');
  const nameInput = screen.getByPlaceholderText('Name your pot');

  fireEvent.change(idInput, { target: { value: '123456' } });
  fireEvent.change(nameInput, { target: { value: 'My Smart Pot' } });

  expect(idInput).toHaveValue('123456');
  expect(nameInput).toHaveValue('My Smart Pot');

  fireEvent.click(screen.getByText('Connect Smart-pot'));

  await waitFor(() => {
    expect(createPot).toHaveBeenCalledWith('My Smart Pot', '123456', null, expect.any(Function));
    expect(navigateMock).toHaveBeenCalledWith('/');
  });
});

test('Connect pot with empty fields', () => {
  const { getByText, getByPlaceholderText } = render(<ConnectPot/>);

  fireEvent.change(getByPlaceholderText('Smart-pot ID'), { target: { value: '' } });
  fireEvent.change(getByPlaceholderText('Name your pot'), { target: { value: '' } });

  fireEvent.click(getByText('Connect Smart-pot'));

  expect(getByText("Smart-pot ID and Pot name are required")).toBeInTheDocument();
});

test('Connect a pot when there is already an existing pot', async () => {
  const navigateMock = jest.fn(); 
  useNavigate.mockReturnValue(navigateMock);
  createPot.mockResolvedValueOnce('MachineID already exists. Cannot create pot.');

  render(
    <BrowserRouter>
      <ConnectPot />
    </BrowserRouter>
  );

  const idInput = screen.getByPlaceholderText('Smart-pot ID');
  const nameInput = screen.getByPlaceholderText('Name your pot');

  fireEvent.change(idInput, { target: { value: '123456' } });
  fireEvent.change(nameInput, { target: { value: 'My Smart Pot' } });

  expect(idInput).toHaveValue('123456');
  expect(nameInput).toHaveValue('My Smart Pot');

  fireEvent.click(screen.getByText('Connect Smart-pot'));

  await waitFor(() => {
    expect(createPot).toHaveBeenCalledWith('My Smart Pot', '123456', null, expect.any(Function));
    expect(screen.getByText('MachineID already exists. Cannot create pot.')).toBeInTheDocument();
    expect(navigateMock).not.toHaveBeenCalled();
  });
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

test('Selecting a plant template in the pop-up', async () => {
  getAllPlants.mockResolvedValueOnce(mockPlants);

  render(
    <BrowserRouter>
      <ConnectPot />
    </BrowserRouter>
  );

  const addButton = screen.getByText('Add a plant');
  fireEvent.click(addButton);

  // Wait for the plant templates to be rendered
  await waitFor(() => {
    expect(screen.getAllByTestId('plant-template')).toHaveLength(mockPlants.length);
  });

  const plantTemplate = screen.getAllByTestId('plant-template')[0];
  fireEvent.click(plantTemplate);

  // After clicking on a plant template, the pop-up should close
  await waitFor(() => expect(screen.queryByText('plantAdd-pop-up')).not.toBeInTheDocument());

  // Verify if the selected plant data is updated accordingly here
  // For example, check if the plant name is displayed
  await waitFor(() => {
    expect(screen.getByText('Rose')).toBeInTheDocument();
  });
});

