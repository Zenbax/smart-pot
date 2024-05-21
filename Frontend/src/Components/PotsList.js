import React, { useState } from 'react';
import Smartpot from '../Components/SmartPot';
import '../Styling/PotsList.css'
import '../Styling/Scrollbar.css'

const PotsList = ({ pots }) => {
    const [listOfPots, setListOfPots] = useState(pots)
    const handleChange = (e) =>{
        var filteredPots = []
        if(e){
             pots.map((pot) =>{
            if(pot.plant.nameOfPlant.toLowerCase().includes(e.toLowerCase())||pot.nameOfPot.toLowerCase().includes(e.toLowerCase())){
                filteredPots.push(pot)
            }
        })
        }
        else{
            filteredPots = pots;
        }
       
        setListOfPots(filteredPots);
    }

    return (
        <>
        
           <div id='search'>
            <form>
                <input onChange={(e)=>handleChange(e.target.value)} type='text' placeholder='search for pot or plant name'/>
            </form>
        </div> 
    
        
        <div id='list'>

            { listOfPots.map((e) => {
                return <Smartpot potID={e.id} key={e.id} pot={e} />;
            })}
        </div>
        </>
        
    );
};


export default PotsList
