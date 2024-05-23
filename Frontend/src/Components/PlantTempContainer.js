import React, { useState, useEffect } from 'react';
import PlantTemp from './PlantTemplate';
import { getAllPlants } from "../Util/API_config";
import '../Styling/PlantTempContainer.css';
import { useAuth } from '../Util/AuthProvider';

const PlantTempContainer = ({ onSelectTemplate }) => {
  const [plants, setPlants] = useState([]);
  const { setToken } = useAuth();
  const [filteredPlants, setFilteredPlants] = useState([]);

  const handleChange = (e) => {
    const searchTerm = e.target.value.toLowerCase();
    const filtered = plants.filter(plant => plant.nameOfPlant.toLowerCase().includes(searchTerm));
    setFilteredPlants(filtered);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const plantsData = await getAllPlants();
        setPlants(plantsData);
        setFilteredPlants(plantsData);
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
          <input onChange={handleChange} type='text' placeholder='search for plant'/>
        </form>
      </div>
      <div className="Plant-Temp-Container row">
        {filteredPlants.map((plant, index) => (
          <PlantTemp key={index} templateData={plant} onSelectTemplate={onSelectTemplate} data-testid="plant-template" />
        ))}
      </div>
    </>
  );
};

export default PlantTempContainer;
  