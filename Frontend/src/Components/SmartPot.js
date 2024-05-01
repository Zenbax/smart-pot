import React from 'react';
import { Link } from 'react-router-dom';


const SmartPot = ({pot}) => {
    return(
        <Link to={"/"+pot.id}>
        <div>
            <h2>{pot.nameOfPot}</h2>
        </div>
        </Link>
        
    );
}


export default SmartPot


