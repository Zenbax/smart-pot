import React from 'react';

const HelpContent = ({ onClose }) => {
  return (
    <div className="popup">
      <div className="popup-content">
        <span className="close" onClick={onClose}>&times;</span>
        <p>This is the help popup content.</p>
      </div>
    </div>
  );
};

export default PopupContent;
