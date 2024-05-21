import React from 'react';
import Smartpot from '../Components/SmartPot';
import '../Styling/PotsList.css'
import '../Styling/Scrollbar.css'

const PotsList = ({ pots }) => {
    return (
        <div id='list'>
            {pots.map((e) => {
                return <Smartpot potID={e.id} key={e.id} pot={e} />;
            })}
        </div>
    );
};


export default PotsList
