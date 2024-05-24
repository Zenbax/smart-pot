
import { BrowserRouter } from "react-router-dom";
import Routes from "./Util/routes.js";
import Navbar from './Components/NavBar';
import './Styling/Navbar.css';
import './Styling/App.css'
import AuthProvider from './Util/AuthProvider.js';
const App = () => {

      return (
            <div id='screen'>
                  <AuthProvider>
                        <BrowserRouter>
                              <Navbar />
                        </BrowserRouter>
                        <div id='content'>
                              <Routes />
                        </div>
                  </AuthProvider>
            </div>
      )
}
export default App;

