import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "./AuthProvider";

export const ProtectedRoute = () => {
  const { token } = useAuth();
  if (!token) {
    return <Navigate to="/smart-pot/Login" />;
  }
  return <Outlet />;
};

