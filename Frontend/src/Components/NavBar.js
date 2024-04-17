/*import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';


const Navbar = () =>
{
    const navbar = document.getElementById('navbar');
    const toggler = navbar.querySelector('.navbar-toggler');
    const collapse = navbar.querySelector('.collapse');
    
    toggler.addEventListener('click', () => {
      collapse.classList.toggle('show');
    });

   
    return(
        <nav id="navbar" class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">Navbar</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="#">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Logout</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    )

}

*/

import React from 'react';

const Navbar=()=> {
  
   return (
      <nav style={{ backgroundColor: '#333', color: 'white', padding: '10px 20px', display: 'flex', justifyContent: 'space-between' }}>
        <div style={{ fontSize: '1.5em' }}>Smart-Pot</div>
        <button style={{ backgroundColor: 'red', color: 'white', border: 'none', padding: '8px 16px', cursor: 'pointer', borderRadius: '4px' }}>Logout</button>
      </nav>
    );


}

export default Navbar

