import React from 'react'
import Button from "react-bootstrap/Button";
import { loginRequest } from "../authConfig";
import { IPublicClientApplication } from "@azure/msal-browser";

interface SigninState {
    instance: IPublicClientApplication,
    isAuthenticated: boolean,
}

interface Properties {
    //children: JSX.Element[] | JSX.Element
    isAuthenticated: boolean,
    msalInstance: IPublicClientApplication
}

export class SignInButton extends React.Component<Properties, SigninState>
{

    constructor(props: any) {
        super(props);
        this.state = {
            instance: props.msalInstance,
            isAuthenticated: props.isAuthenticated
        };
    }

    handleLogin(instance: any) {
        instance.loginPopup(loginRequest).catch((e:any) => {
            console.error(e);
        });

    }

    render() {
        return <Button variant="secondary" className="ml-auto" onClick={() => this.handleLogin(this.state.instance)}>Sign In</Button>
    }

}