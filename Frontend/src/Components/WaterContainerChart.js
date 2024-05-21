// WaterContainerChart.jsx
import React from 'react';
import PropTypes from 'prop-types';
import '../Styling/WaterContainerChart.css';

const WaterContainerChart = ({ currentWaterLevel}) => {
    return (
        <div className="water-container-wrapper">
            <div>
                {currentWaterLevel} %
            </div>
            <div className="water-container">
                <div className="water-bar" style={{ height: `currentWaterLevel%` }}></div>
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
