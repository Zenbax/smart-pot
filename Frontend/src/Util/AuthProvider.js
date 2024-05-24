import axios from "axios";
import { createContext, useContext, useEffect, useMemo, useState } from "react";

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  const [token, setToken_] = useState(localStorage.getItem("token"));

  
  const setToken = (newToken) => {
    setToken_(newToken);

  };

  useEffect(() => {
    if (!token) {
      delete axios.defaults.headers.common["Authorization"];
      localStorage.removeItem("token");
      localStorage.removeItem("user")
      console.log("Token removed!")
    }
  }, [token]);

  
  const contextValue = useMemo(
    () => ({
      token,
      setToken,
    }),
    [token]
  );

  
  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};

export default AuthProvider;