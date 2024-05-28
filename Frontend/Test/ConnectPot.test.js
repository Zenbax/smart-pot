import React from 'react';
import { render, screen, fireEvent, waitFor, getByText } from '@testing-library/react';
import '@testing-library/jest-dom';
import ConnectPot from '../src/Pages/ConnectPot';
import { BrowserRouter, useNavigate } from 'react-router-dom';
import { getAllPlants, createPot } from '../src/Util/apiClient';

jest.mock('../src/Util/apiClient', () => ({
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
    expect(navigateMock).toHaveBeenCalledWith('/smart-pot');
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
    expect(screen.queryByText(/Plant Name/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Minimum Soil Moisture/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Watering Amount \(ml\)/i)).not.toBeInTheDocument();
  });
});

test('Searching for a plant in plant template', async () => {
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

  const searchInput = screen.getByPlaceholderText('search for plant');
  fireEvent.change(searchInput, { target: { value: 'rose' } });

  // Wait for the search results to be filtered
  await waitFor(() => {
    expect(screen.getByText(/Rose/i)).toBeInTheDocument();
    expect(screen.queryByText(/Tulip/i)).not.toBeInTheDocument();
  });
});

test('Removing a plant template', async () => {
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

  fireEvent.click(screen.getByText(/Rose/i));
  // After clicking on a plant template, the plant should be selected and the popup should close

  await waitFor(() => expect(screen.queryByText('plantAdd-pop-up')).not.toBeInTheDocument());
  expect(screen.getByText(/Rose/i)).toBeInTheDocument();

  // Verify the button text has changed to "Change plant"
  const changeButton = screen.getByText("Change plant");
  expect(changeButton).toBeInTheDocument();

  fireEvent.click(changeButton);

  // Click the remove button to remove the selected plant
  const removeButton = screen.getByText('Remove plant');
  fireEvent.click(removeButton);

  // After clicking remove, the plant should be removed and the popup should close
  await waitFor(() => expect(screen.queryByText('plantAdd-pop-up')).not.toBeInTheDocument());

  // Verify the button text has reverted to "Add a plant"
  expect(screen.getByText('Add a plant')).toBeInTheDocument();
});

test("Create New goes to plantoverviwe", async () => {
  getAllPlants.mockResolvedValueOnce(mockPlants);
  const navigateMock = jest.fn();
  useNavigate.mockReturnValue(navigateMock);

  render(
    <BrowserRouter>
      <ConnectPot />
    </BrowserRouter>
  );

  const addButton = screen.getByText('Add a plant');
  fireEvent.click(addButton);

  await waitFor(() => {
    expect(screen.getAllByTestId('plant-template')).toHaveLength(mockPlants.length);
  });

  const createNewButton = screen.getByText("Create New");
  fireEvent.click(createNewButton);

  await waitFor(() => {
    expect(navigateMock).toHaveBeenCalledWith('/smart-pot/plant_overview');
  });

})


test('Back button goes back', async () => {
  const navigateMock = jest.fn();
  useNavigate.mockReturnValue(navigateMock);
  const historyBackMock = jest.fn();
  global.window.history.back = historyBackMock;

  render(
    <BrowserRouter>
      <ConnectPot />
    </BrowserRouter>
  );
  
  fireEvent.click(screen.getByText('Back'));

  await waitFor(() => {
    expect(historyBackMock).toHaveBeenCalled();
  });
});

