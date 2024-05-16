// WaterContainerChart.jsx
import React from 'react';
import PropTypes from 'prop-types';
import '../Styling/WaterContainerChart.css';

const WaterContainerChart = ({ currentWaterLevel, maxWaterLevel }) => {
    const waterLevelPercentage = (currentWaterLevel / maxWaterLevel) * 100;

    return (
        <div className="water-container-wrapper">
            <div>
                {currentWaterLevel} mL
            </div>
            <div className="water-container">
                <div className="water-bar" style={{ height: `${waterLevelPercentage}%` }}></div>
            </div>
            <p className="water-container-text">
                Water
            </p>
        </div>
    );
};

WaterContainerChart.propTypes = {
    currentWaterLevel: PropTypes.number.isRequired,
    maxWaterLevel: PropTypes.number.isRequired,
};

export default WaterContainerChart;
