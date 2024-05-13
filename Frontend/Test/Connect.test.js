import Connect_pot from "../src/Pages/ConnectPot"
import {render, screen} from '@testing-library/react';
import React from "react";
import '@testing-library/jest-dom';

test('Render Connect_pot correctly', () => { 
    render (<Connect_pot/>)

    expect(screen.getByText('Connect your Smart-pot')).toBeInTheDocument();

    //Todo: PlantTemp tror lav en for den selv.

    

 })