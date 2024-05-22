import React from 'react';

import { render, fireEvent, screen, waitFor, getByText, getByTitle } from '@testing-library/react';
import '@testing-library/jest-dom';
import userEvent from '@testing-library/user-event';
import PotDataChart from '../src/Components/PotDataChart'
import PotDetails from '../src/Pages/PotDetails'
import { getPotFromId } from '../src/API/API_config';

jest.mock('../src/Util/API_config', () => ({
    getPotFromId: jest.fn().mockResolvedValue({
      NameOfpot: 'Test Pot',
      VandingsLog: [],
      Fugtighedslog: []
    })
  }));
  


test('render Pot Details correctly', async () => { 
    render (<PotDetails potID= "pot_id"/>);

    await waitFor(() => expect(getPotFromId).toHaveBeenCalledTimes(1)) 

        
    expect(screen.getByText('Test Pot')).toBeInTheDocument();
    
      

 });


 test('view toggle updates chart', async () => {
    render(<PotDetails />);

    // Wait for API call to finish
    await waitFor(() => expect(getPotFromId).toHaveBeenCalledTimes(4));

    // Click on "View by months" button
    const viewByMonthsButton = screen.getByText('View by months');
    userEvent.click(viewByMonthsButton);

    const updatedChart = screen.getByTestId('pot-data-chart');
    expect(updatedChart).toBeInTheDocument();
 });

