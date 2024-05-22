import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom';
import ConnectPot from '../src/Pages/ConnectPot';
import { BrowserRouter } from 'react-router-dom';


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

test('Render ConnectPot correctly', () => { 
    render(<ConnectPot/>);

    expect(screen.getByText('Connect your Smart-pot')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Smart-pot ID')).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Name your pot')).toBeInTheDocument();
    expect(screen.getByText("Add a plant")).toBeInTheDocument();
    expect(screen.getByText("Connect Smart-pot")).toBeInTheDocument();
    expect(screen.getByText("Back")).toBeInTheDocument();
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

test('Selecting a plant template in the pop-up', async () => {
  render(
      <BrowserRouter>
          <ConnectPot />
      </BrowserRouter>
  );

  const addButton = screen.getByText('Add a plant');
  fireEvent.click(addButton);

  // Wait for the plant templates to be rendered
  await waitFor(() => {
      expect(screen.getAllByTestId('plant-template')).toHaveLength(1); // Adjust length based on the expected number of templates
  });

  const plantTemplate = screen.getAllByTestId('plant-template')[0];
  fireEvent.click(plantTemplate);

  // After clicking on a plant template, the pop-up should close
  await waitFor(() => expect(screen.queryByText('Plants')).not.toBeInTheDocument());

  // Verify if the selected plant data is updated accordingly here
  // For example, check if the plant name is displayed
  await waitFor(() => {
      expect(screen.getByText(/Plant Name:/i)).toBeInTheDocument();
  });
});



