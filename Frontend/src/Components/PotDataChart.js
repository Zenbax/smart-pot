import React, { useEffect, useRef, useState } from 'react';
import Chart from 'chart.js/auto';
import { getPotFromId } from '../API/API_config';

function getWeekNumber(date) {
  const adjustedDate = new Date(date.getTime());
  adjustedDate.setHours(0, 0, 0, 0);
  adjustedDate.setDate(adjustedDate.getDate() + 3 - ((adjustedDate.getDay() + 6) % 7));
  const firstWeek = new Date(adjustedDate.getFullYear(), 0, 4);
  return 1 + Math.round(((adjustedDate.getTime() - firstWeek.getTime()) / 86400000 - 3 + (firstWeek.getDay() + 6) % 7) / 7);
}

// Helper function to generate ranges
function generateDateLabels(type, count) {
  const labels = [];
  const now = new Date();

  switch (type) {
    case 'years':
      for (let i = 0; i < count; i++) {
        labels.unshift((now.getFullYear() - i).toString());
      }
      break;

    case 'months':
      for (let i = 0; i < count; i++) {
        const month = now.getMonth() - i;
        const date = new Date(now.getFullYear(), month, 1);
        labels.unshift(`${date.getFullYear()}-${date.getMonth() + 1}`);
      }
      break;

    case 'weeks':
      for (let i = 0; i < count; i++) {
        const weekDate = new Date(now);
        weekDate.setDate(now.getDate() - (i * 7));
        labels.unshift(`${weekDate.getFullYear()}-W${getWeekNumber(weekDate)}`);
      }
      break;

    case 'days':
      for (let i = 0; i < count; i++) {
        const dayDate = new Date(now);
        dayDate.setDate(now.getDate() - i);
        labels.unshift(dayDate.toISOString().split('T')[0]);
      }
      break;
  }

  return labels;
}

const PotDataChart = ({ potID }) => {
  const chartRef = useRef(null);
  const chartInstanceRef = useRef(null); // Ref to store the chart instance
  const [potData, setPotData] = useState(null);
  const [viewBy, setViewBy] = useState('days');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getPotFromId(potID);
        if (response.success) {
          setPotData(response.pot.sensorData || []);
        } else {
          console.error('Error fetching data:', response.message);
        }
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchData();
  }, [potID]);

  useEffect(() => {
    if (potData) {
      const ctx = chartRef.current.getContext('2d');

      const labelCount = viewBy === 'weeks' ? 6 : 5;
      const intervalLabels = generateDateLabels(viewBy, labelCount);

      const aggregatedWaterData = {};
      const aggregatedMoistureData = {};
      intervalLabels.forEach(label => {
        aggregatedWaterData[label] = 0;
        aggregatedMoistureData[label] = { sum: 0, count: 0 };
      });

      // Aggregate sensor data
      potData.forEach(entry => {
        const date = new Date(entry.timestamp);
        let label;

        switch (viewBy) {
          case 'years':
            label = date.getFullYear().toString();
            break;
          case 'months':
            label = `${date.getFullYear()}-${date.getMonth() + 1}`;
            break;
          case 'weeks':
            label = `${date.getFullYear()}-W${getWeekNumber(date)}`;
            break;
          case 'days':
            label = date.toISOString().split('T')[0];
            break;
        }

        if (aggregatedWaterData[label] !== undefined) {
          aggregatedWaterData[label] += entry.amountOfWatering;
        }
        if (aggregatedMoistureData[label] !== undefined) {
          aggregatedMoistureData[label].sum += entry.measuredSoilMoisture;
          aggregatedMoistureData[label].count++;
        }
      });

      const sumAmountOfWateringData = intervalLabels.map(label => aggregatedWaterData[label]);
      const averageMoistureData = intervalLabels.map(label => aggregatedMoistureData[label].sum / aggregatedMoistureData[label].count);

      if (chartInstanceRef.current) {
        chartInstanceRef.current.destroy();
      }

      const newChart = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: intervalLabels,
          datasets: [
            {
              label: 'Measured Soil Moisture',
              data: averageMoistureData,
              backgroundColor: 'lightblue',
              borderColor: 'rgba(54, 162, 235, 1)',
              borderWidth: 1,
              type: 'line'
              
            },
            {
              label: 'Amount of Watering (mL)',
              data: sumAmountOfWateringData,
              backgroundColor: 'pink',
              borderColor: 'rgba(255, 99, 132, 1)',
              borderWidth: 1
            }
          ]
        },
        options: {
          responsive: true,
          scales: {
            y: {
              beginAtZero: true,
              title: {
                display: true,
                text: 'Value'
              }
            },
            x: {
              title: {
                display: true,
                text: `Time Interval (${viewBy})`
              }
            }
          }
        }
      });

      chartInstanceRef.current = newChart;
    }
  }, [potData, viewBy]);

  const toggleView = (view) => {
    setViewBy(view);
  };

  return (
    <div>
      <canvas ref={chartRef } data-testid="pot-data-chart"/>
      <div>
        <button onClick={() => toggleView('years')}>View by years</button>
        <button onClick={() => toggleView('months')}>View by months</button>
        <button onClick={() => toggleView('weeks')}>View by weeks</button>
        <button onClick={() => toggleView('days')}>View by days</button>
      </div>
    </div>
  );
};

export default PotDataChart;
