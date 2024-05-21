import React, { useState, useEffect } from 'react';
import PlantTemp from './PlantTemplate';
import { getAllPlants } from "../Util/API_config";
import '../Styling/PlantTempContainer.css'
import { useAuth } from '../Util/AuthProvider';


const PlantTempContainer = ({ onSelectTemplate }) => {

    const [plants, setPlants] = useState([]);
    const { setToken } = useAuth();
    const [FilteredListListOfPlants, setFilteredListOfplants] = useState([])
    const handleChange = (e) =>{
        var filteredPlants = []
        console.log(filteredPlants)
        if(e){
             plants.map((plant) =>{
            if(plant.nameOfPlant.toLowerCase().includes(e.toLowerCase())){
                filteredPlants.push(plant)
            }
        })
        }
        else{
            filteredPlants = plants;
        }
        setFilteredListOfplants(filteredPlants);
    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                const plantsData = await getAllPlants(setToken);
                setPlants(plantsData);
                setFilteredListOfplants(plantsData);
            } catch (error) {
                console.error('Error fetching plants:', error);
            }
        };
        fetchData();
    }, []);

    return (
        <>
         <div id='search'>
                <form>
                <input onChange={(e)=>handleChange(e.target.value)} type='text' placeholder='search for plant'/>
            </form>
            </div>
            
        <div className="Plant-Temp-Container row">
           
            {FilteredListListOfPlants && FilteredListListOfPlants.map((plant, index) => (
                <div key={index} className="col-lg-4">
                    <PlantTemp templateData={plant} onSelectTemplate={onSelectTemplate} />
                </div>
            ))}
        </div>
        </>
        
    );
};

export default PlantTempContainer;
