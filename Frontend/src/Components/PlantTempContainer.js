import React, { useState, useEffect } from 'react';
import PlantTemp from './PlantTemplate';
import { getAllPlants } from "../Util/API_config";
import '../Styling/PlantTempContainer.css'
import { useAuth } from '../Util/AuthProvider';


const PlantTempContainer = ({ onSelectTemplate }) => {

    const [plants, setPlants] = useState([]);
    const { setToken } = useAuth();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const plantsData = await getAllPlants(setToken);
                setPlants(plantsData);
            } catch (error) {
                console.error('Error fetching plants:', error);
            }
        };
        fetchData();
    }, []);

    return (
        <div className="Plant-Temp-Container row">
            {plants && plants.map((plant, index) => (
                <div key={index} className="col-lg-4">
                    <PlantTemp templateData={plant} onSelectTemplate={onSelectTemplate} />
                </div>
            ))}
        </div>
    );
};

export default PlantTempContainer;
