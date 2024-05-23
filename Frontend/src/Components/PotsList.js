import React, { useState } from 'react';
import Smartpot from '../Components/SmartPot';
import '../Styling/PotsList.css';
import '../Styling/Scrollbar.css';

const PotsList = ({ pots }) => {
    const [listOfPots, setListOfPots] = useState(pots);

    const handleChange = (e) => {
        const searchTerm = e.toLowerCase();
        const filteredPots = pots.filter(pot =>
            pot.plant?.nameOfPlant?.toLowerCase().includes(searchTerm) ||
            pot.nameOfPot.toLowerCase().includes(searchTerm)
        );
        setListOfPots(filteredPots);
    };

    return (
        <div>
            <div id='search'>
                <form>
                    <input onChange={(e) => handleChange(e.target.value)} type='text' placeholder='search for pot or plant' />
                </form>
            </div>

            <div id='list'>
                {listOfPots.map((e) => (
                    <Smartpot data-testid="smartPodContainer" potID={e.id} key={e.id} pot={e} />
                ))}
            </div>
        </div>
    );
};

export default PotsList;
