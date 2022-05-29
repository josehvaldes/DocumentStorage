import { Route, Navigate } from 'react-router-dom';
import { useRecoilValue } from 'recoil';
import { useIsAuthenticated } from "@azure/msal-react";

export { PrivateRoute };

function PrivateRoute({ children }) {
    const isAuthenticated = useIsAuthenticated();
    return isAuthenticated? children : <Navigate to="/success" />
}

