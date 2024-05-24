import React from 'react';
import { render, fireEvent, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import userEvent from '@testing-library/user-event';
import PotDetails from '../src/Pages/PotDetails';
import { getPotFromId, deletePot, updatePot, getAllPlants} from '../src/Util/apiClient';
import { useAuth } from '../src/Util/AuthProvider';
import { BrowserRouter, useNavigate } from 'react-router-dom';



jest.mock('../src/Util/apiClient', () => ({
  getPotFromId: jest.fn(),
  deletePot: jest.fn(),
  updatePot: jest.fn(),
  getAllPlants: jest.fn(),
}));

// Mock pot data
const mockPotData = {
  success: true,
  pot: {
    nameOfPot: 'Test Pot',
    email: 'test@example.com',
    machineID: 'machine123',
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
  plants: [
    {
      nameOfPlant: 'Rose',
      image: 'rose.jpg',
      soilMinimumMoisture: 50,
      amountOfWaterToBeGiven: 200,
    },
    {
      nameOfPlant: 'Tulip',
      image: 'tulip.jpg',
      soilMinimumMoisture: 60,
      amountOfWaterToBeGiven: 150,
    },
  ],
};


// Mock useParams and useNavigate hooks
jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({ potID: 'pot123' }),
  useNavigate: jest.fn(),
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
    getAllPlants.mockResolvedValueOnce(mockPotData.plants);
  });

  test('renders Pot Details with fetched data', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    await waitFor(() => expect(screen.getByText('Test Pot')).toBeInTheDocument());
    expect(screen.getByText('Moisture percentage: 50')).toBeInTheDocument();
    expect(screen.getByText('Plant: Rose')).toBeInTheDocument();
    expect(screen.getByText('Minimum Moisture: 50')).toBeInTheDocument();
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
  
    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());
  
    const toggleDisable = screen.getByRole('checkbox', { name: 'Disable watering' });
    expect(toggleDisable).not.toBeChecked();

    const initialPotData = await getPotFromId.mock.results[0].value;
    expect(initialPotData.pot.enable).toBeTruthy();
  
    fireEvent.click(toggleDisable);
  
    expect(toggleDisable).toBeChecked();
  
    const updatedPotData = await getPotFromId.mock.results[0].value;
    expect(updatedPotData.pot.enable).toBe(1);      
  });
  



  test('PotDetails: Edit plant', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    expect(screen.getByText('Minimum Moisture: 50')).toBeInTheDocument();
    expect(screen.getByText('mL per watering: 200')).toBeInTheDocument();
  
    fireEvent.change(screen.getByPlaceholderText('Enter minimum moisture'), { target: { value: '40' } });
    fireEvent.change(screen.getByPlaceholderText('Enter watering amount'), { target: { value: '60' } });
    
    fireEvent.click(screen.getByText('Save Changes'));

    expect(screen.getByText('Minimum Soil Moisture: 40')).toBeInTheDocument();
    expect(screen.getByText('Watering Amount (ml): 60')).toBeInTheDocument();
  });


  test('PotDetails: Edit plant with same value', async () => {
    render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    expect(screen.getByText('Minimum Moisture: 50')).toBeInTheDocument();
    expect(screen.getByText('mL per watering: 200')).toBeInTheDocument();
  
    fireEvent.change(screen.getByPlaceholderText('Enter minimum moisture'), { target: { value: '50' } });
    fireEvent.change(screen.getByPlaceholderText('Enter watering amount'), { target: { value: '200' } });
    
    fireEvent.click(screen.getByText('Save Changes'));

    expect(screen.getByText('Values have not changed')).toBeInTheDocument();
  });

test('PotDetails: Edit plant below soil moisture minimum value', async () => {
  render(<PotDetails />);

  await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

  fireEvent.change(screen.getByPlaceholderText('Enter watering amount'), { target: { value: '10' } });
  
  fireEvent.click(screen.getByText('Save Changes'));

  expect(screen.getByText('Watering amount must be 20ml or higher')).toBeInTheDocument();

});

test('change plant', async () => {
 render(<PotDetails />);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    expect(screen.getByText('Plant: Rose')).toBeInTheDocument();

    fireEvent.click(screen.getByText('Change plant'));
    
    await waitFor(() => expect(screen.getByTestId('plant-add-popup')).toBeInTheDocument());

    fireEvent.click(screen.getByText('Tulip'));

    fireEvent.click(screen.getByText('Save Changes'));

    await waitFor(() => {
      expect(screen.getByText('Plant: Tulip')).toBeInTheDocument();
      expect(screen.getByText('Minimum Moisture: 60')).toBeInTheDocument();
      expect(screen.getByText('mL per watering: 150')).toBeInTheDocument();
  });
});


test('PotDetails: Disconnect pot', async () => {
  const navigateMock = jest.fn();
  useNavigate.mockReturnValue(navigateMock);

  render(
    <BrowserRouter>
      <PotDetails />
    </BrowserRouter>
  );

    await waitFor(() => expect(getPotFromId).toHaveBeenCalled());

    fireEvent.click(screen.getByText('Disconnect pot'));

    await waitFor(() => expect(screen.getByText("You are about to delete: Test Pot")).toBeInTheDocument());

    fireEvent.click(screen.getByText('Delete'));

    await waitFor(() => {
      expect(navigateMock).toHaveBeenCalledWith("/");
  });
});

test('Back button goes back', async () => {
  const navigateMock = jest.fn();
  useNavigate.mockReturnValue(navigateMock);
  const historyBackMock = jest.fn();
  global.window.history.back = historyBackMock;

  render(
    <BrowserRouter>
      <PotDetails />
    </BrowserRouter>
  );
  
  fireEvent.click(screen.getByText('Back'));

  await waitFor(() => {
    expect(historyBackMock).toHaveBeenCalled();
  });
});


});



