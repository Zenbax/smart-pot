import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import Login from "../Pages/Login";
import Home from '../Pages/Home';
import Register from '../Pages/Register';
import ConnectPot from '../Pages/ConnectPot';
import PlantOverview from '../Pages/PlantOverview.js';
import PotDetails from "../Pages/PotDetails.js";

const Routes = () => {
  const { token } = useAuth();

  // Define public routes accessible to all users
  const routesForPublic = [];

  // Define routes accessible only to authenticated users
  const routesForAuthenticatedOnly = [
    {
      path: "/",
      element: <ProtectedRoute />, // Wrap the component in ProtectedRoute
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

  // Define routes accessible only to non-authenticated users
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

  // Combine and conditionally include routes based on authentication status
  const router = createBrowserRouter([
    ...routesForPublic,
    ...(!token ? routesForNotAuthenticatedOnly : []),
    ...routesForAuthenticatedOnly,
  ]);

  // Provide the router configuration using RouterProvider
  return <RouterProvider router={router} />;
};

export default Routes;