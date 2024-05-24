import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import Login from "../Pages/Login";
import Home from '../Pages/Home';
import Register from '../Pages/Register';
import ConnectPot from '../Pages/ConnectPot';
import PlantOverview from '../Pages/PlantOverview.js';
import PotDetails from "../Pages/PotDetails.js";
import PageNotFound from "../Pages/PageNotFound.js"

const Routes = () => {
  const { token } = useAuth();


  const routesForPublic = [
    {
      path: "*",
      element: <PageNotFound />,
    },
  ];


  const routesForAuthenticatedOnly = [
    {
      path: "/",
      element: <ProtectedRoute />, 
      children: [
        {
          path: "/",
          element: <Home />,
        },
        {
          path: "/plant_overview",
          element: <PlantOverview />,
        },
        {
          path: "/connect_pot",
          element: <ConnectPot />,
        },
        {
            path: "/pot-details/:potID",
            element: <PotDetails/>,
          },
      ],
    },
  ];

 
  const routesForNotAuthenticatedOnly = [
    {
      path: "/register",
      element: <Register />,
    },
    {
      path: "/login",
      element: <Login/>,
    },
  ];

 
  const router = createBrowserRouter([
    ...routesForPublic,
    ...(!token ? routesForNotAuthenticatedOnly : []),
    ...routesForAuthenticatedOnly,
  ]);


  return <RouterProvider router={router} />;
};

export default Routes;