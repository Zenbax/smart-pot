import React from 'react';

const WaterContainerChart = ({ currentWaterLevel, maxWaterLevel }) => {
  // Calculate the percentage of water level
  const percentage = (currentWaterLevel / maxWaterLevel) * 100;

  // Style for the bar
  const barStyle = {
    backgroundColor: 'lightblue',
    width: '20px', // Width represents the thickness of the vertical bar
    height: `${percentage}%`,
    transition: 'height 0.5s ease-in-out' // Optional: Add transition effect
  };

  // Style for the empty part of the bar
  const emptyStyle = {
    width: '20px',
    height: `${100 - percentage}%`,
    backgroundColor: 'transparent',
    border: '1px solid black'
  };

  return (
    <div style={{ display: 'inline-block', marginRight: '10px', textAlign: 'center' }}>
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <div style={emptyStyle}></div>
        <div style={barStyle}></div>
      </div>
      <div style={{ marginTop: '5px' }}>{`${percentage.toFixed(2)}%`}</div>
    </div>
  );
};

export default WaterContainerChart;
