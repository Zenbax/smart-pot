import React from 'react';
import PlantTemp from '../Components/PlantTemplate';

const PlantOverview = () => {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-lg-6">
          <h1 className="text-center mb-4">Plants</h1>
          <div className="row">
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
            <div className="col-lg-4">
              <PlantTemp />
            </div>
          </div>
        </div>
        <div className="col-lg-3">

          {
            // tilføj template form
          }

        </div>
      </div>
    </div>
  );
};

export default PlantOverview;
