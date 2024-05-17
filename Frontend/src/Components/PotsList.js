import React from 'react';
import Smartpot from '../Components/SmartPot';
import '../Styling/PotsList.css'
import '../Styling/Scrollbar.css'

const PotsList = ({ pots }) => {
    return (
        <div id='list'>
            {pots.map((e) => {
                return <Smartpot key={e.nameOfPot} pot={e} />;
            })}
        </div>
    );
};


export default PotsList
