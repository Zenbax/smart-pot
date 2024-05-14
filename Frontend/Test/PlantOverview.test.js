import PlantOverview from "../src/Pages/PlantOverview"
import {render, screen} from '@testing-library/react';
import React from "react";
import '@testing-library/jest-dom';

test('Render PlantOverView correctly', () => { 
    render (<PlantOverview/>)

    expect(screen.getByText('Back')).toBeInTheDocument();
    expect(screen.getByText('Plants')).toBeInTheDocument();
    expect(screen.getAllByTestId('plant-template')[0]).toBeInTheDocument();
    expect(screen.getByText('Edit Plant')).toBeInTheDocument();
    expect(screen.getByText('Plant Name:')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter plant name")).toBeInTheDocument();
    expect(screen.getByText('Minimum Soil Moisture:')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter minimum moisture")).toBeInTheDocument();
    expect(screen.getByText('Watering Amount (ml):')).toBeInTheDocument();
    expect(screen.getByPlaceholderText("Enter watering amount")).toBeInTheDocument();
    expect(screen.getByText('Upload a picture')).toBeInTheDocument();
    expect(screen.getByText('Save Changes')).toBeInTheDocument();    

 })

