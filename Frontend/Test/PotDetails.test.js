import React from 'react';
import { render, fireEvent, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import userEvent from '@testing-library/user-event';
import PotDetails from '../src/Pages/PotDetails';
import { getPotFromId, deletePot, updatePot } from '../src/Util/API_config';

jest.mock('../src/Util/API_config', () => ({
  getPotFromId: jest.fn(),
  deletePot: jest.fn(),
  updatePot: jest.fn(),
}));

// Mock pot data
const mockPotData = {
  success: true,
  pot: {
    nameOfPot: 'Test Pot',
    email: 'test@example.com',
    machineId: 'machine123',
    enable: 1,
    sensorData: [
      {
        timestamp: '2023-05-01T12:00:00Z',
        measuredSoilMoisture: 45,
        amountOfWatering: 100,
        waterTankLevel: 75,
      },
      {
        timestamp: '2023-05-02T12:00:00Z',
        measuredSoilMoisture: 50,
        amountOfWatering: 120,
        waterTankLevel: 70,
      },
    ],
    plant: {
      nameOfPlant: 'Rose',
      image: 'rose.jpg',
      soilMinimumMoisture: 50,
      amountOfWaterToBeGiven: 200,
    },
  },
};


// Mock useParams and useNavigate hooks
jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({ potID: 'pot123' }),
  useNavigate: () => jest.fn(),
}));

// Mock useAuth hook
jest.mock('../src/Util/AuthProvider', () => ({
  useAuth: () => ({
    setToken: jest.fn(),
    token: 'your_mocked_token_here',
  }),
}));

describe('PotDetails component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
    getPotFromId.mockResolvedValueOnce(mockPotData);
  });

  test('renders Pot Details with fetched data', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    await waitFor(() => expect(screen.getByText('Test Pot')).toBeInTheDocument());
    expect(screen.getByText('Humidity percentage: 50')).toBeInTheDocument();
    expect(screen.getByText('Plant: Rose')).toBeInTheDocument();
    expect(screen.getByText('Minimum Humidity: 50')).toBeInTheDocument();
    expect(screen.getByText('mL per watering: 200')).toBeInTheDocument();
  });

  test('view toggle updates chart', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalledTimes(1));

    const viewByMonthsButton = screen.getByText(/View by months/i);
    userEvent.click(viewByMonthsButton);

    const updatedChart = screen.getByTestId('pot-data-chart');
    expect(updatedChart).toBeInTheDocument();
  });


  test('user interaction: toggle watering settings', async () => {
    render(<PotDetails />);
  
    // Wait for the pot data to be fetched and rendered
    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());
  
    // Check initial state of the pot
    const toggleDisable = screen.getByRole('checkbox', { name: 'Disable watering' });
    expect(toggleDisable).not.toBeChecked(); // Ensure it's not checked initially
  
    // Check initial pot state
    const initialPotData = await getPotFromId.mock.results[0].value;
    expect(initialPotData.pot.enable).toBeTruthy(); // Assuming the pot is enabled initially
  
    // Perform the action to disable the watering
    fireEvent.click(toggleDisable);
  
    // Verify the checkbox is now checked
    expect(toggleDisable).toBeChecked();
  
    // Verify the pot update API call with expected arguments
    await waitFor(() => {
      expect(updatePot).toHaveBeenCalledWith(
        'Test Pot', undefined, undefined, 1, null, 'pot123', 'your_mocked_token_here'
      );
    });
  
    // Check the state after the action
    const updatedPotData = await getPotFromId.mock.results[0].value;
    expect(updatedPotData.pot.enable).toBeFalsy(); // Assuming the pot is disabled now
  });
  

/*
  test('user interaction: toggle watering settings', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    const toggleDisable = screen.getByRole('checkbox', { name: 'Disable watering' });
    fireEvent.click(toggleDisable);

    expect(updatePot).toHaveBeenCalledWith('Test Pot', undefined, undefined, 1, null, 'pot123', 'your_mocked_token_here');
  });
*/


  test('PotDetails: Edit plant', async () => {
    render(<PotDetails />);
  
    // Fill out the form in the PlantAddPopUp component
    fireEvent.change(screen.getByPlaceholderText('Enter minimum moisture'), { target: { value: '40' } });
    fireEvent.change(screen.getByPlaceholderText('Enter watering amount'), { target: { value: '60' } });
    
    // Click "Save Changes" button
    fireEvent.click(screen.getByText('Save Changes'));
  });

  test('PotDetails: Disconnect pot', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    fireEvent.click(screen.getByText('Disconnect pot'));

    fireEvent.click(screen.getByText('Delete'));

    await waitFor (()=>{
      expect(navigateMock).toHaveBeenCalledWith("/");
    })
    
  });
});


