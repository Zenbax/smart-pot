import ConnectPot from "../src/Pages/ConnectPot";
import { render, screen, fireEvent } from '@testing-library/react';
import React from "react";
import '@testing-library/jest-dom';

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
