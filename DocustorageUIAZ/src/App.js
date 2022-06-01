import './App.css';

import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { ProtectedComponent } from "./components/ProtectedComponent";
import { PageLayout, ContentBody } from './layout';


function App() {
  return (
      <PageLayout>
          <AuthenticatedTemplate>
              <ProtectedComponent />
              <ContentBody />
          </AuthenticatedTemplate>
          <UnauthenticatedTemplate>
              <center>You are not signed in! Please sign in to be awesome!</center>
          </UnauthenticatedTemplate>
      </PageLayout>
  );
}

export default App;
