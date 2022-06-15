import React from 'react';
import './App.css';
import { ContentBody } from './layout/ContentBody';
import { Layout } from './layout/Layout';
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { ProtectedComponent } from "./components/ProtectedComponent";
import { useIsAuthenticated } from "@azure/msal-react";
import { authAtom } from './_state';
import { useRecoilState } from 'recoil';
import { useNavigate } from 'react-router-dom';

function App() {

    const isAuthenticated = useIsAuthenticated();
    const { instance, accounts } = useMsal();
    const [auth, setAuth] = useRecoilState(authAtom);

    return (
        <Layout isAuthenticated={isAuthenticated} msalinstance={instance} >
            <AuthenticatedTemplate>
                <ProtectedComponent />
                <ContentBody accountInfo={accounts[0]} authInfo={auth} />
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <p>
                    You are not signed in! Please sign in to be awesome!
                </p>
            </UnauthenticatedTemplate>
        </Layout >    
      );
}

export default App;
