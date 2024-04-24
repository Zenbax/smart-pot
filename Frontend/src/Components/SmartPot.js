import React from 'react';
import { Link } from 'react-router-dom';


const SmartPot = ({pot}) => {
    return(
        <Link to={"/"+pot.PotId}>
        <div>
            <h2>{pot.NameOfpot}</h2>
        </div>
        </Link>
        
    );
}


export default SmartPot


