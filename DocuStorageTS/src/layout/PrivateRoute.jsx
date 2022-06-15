import { Navigate } from 'react-router-dom';
import { useIsAuthenticated } from "@azure/msal-react";

export { PrivateRoute };

function PrivateRoute({ children }) {
    const isAuthenticated = useIsAuthenticated();
    return isAuthenticated? children : <Navigate to="/" />
}

