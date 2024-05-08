import PlantOverview from "../src/Pages/PlantOverview"
import {render, screen} from '@testing-library/react';
import React from "react";
import '@testing-library/jest-dom';

test('Render PlantOverView correctly', () => { 
    render (<PlantOverview/>)

    expect(screen.getByText('Plants')).toBeInTheDocument();

    //Todo: PlantTemp tror lav en for den selv.

    

 })

