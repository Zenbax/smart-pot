import React, { useState, useEffect } from 'react';
import PlantTemp from './PlantTemplate';
import { getAllPlants } from "../API/API_config";

const PlantTempContainer = ({ onSelectTemplate }) => {

    const [plants, setPlants] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const plantsData = await getAllPlants();
                setPlants(plantsData);
            } catch (error) {
                console.error('Error fetching plants:', error);
            }
        };
        fetchData();
    }, []);

    return (
        <div className="row">
            {plants && plants.map((plant, index) => (
                <div key={index} className="col-lg-4">
                    <PlantTemp templateData={plant} onSelectTemplate={onSelectTemplate} />
                </div>
            ))}
        </div>
    );
};

export default PlantTempContainer;
