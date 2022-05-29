import React from "react";
import Navbar from "react-bootstrap/Navbar";
import { useIsAuthenticated } from "@azure/msal-react";
import { SignInButton } from "../components/SignInButton";
import { SignOutButton } from "../components/SignOutButton";
import { useMsal } from "@azure/msal-react";

/**
 * Renders the navbar component with a sign-in button if a user is not authenticated
 */
export const PageLayout = (props) => {
    const isAuthenticated = useIsAuthenticated();
    const { instance, accounts } = useMsal();

    return (
        <div>
            <Navbar bg="dark" variant="dark">
                <a className="navbar-brand" href="/">Document Storage Azure AD</a>
                {isAuthenticated ? <SignOutButton /> : <SignInButton />}
            </Navbar>
            {props.children}
        </div>
    );
};