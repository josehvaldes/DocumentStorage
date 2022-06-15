import React from 'react'
import Button from "react-bootstrap/Button";
import { IPublicClientApplication } from "@azure/msal-browser";

interface SignoutState
{
    instance: IPublicClientApplication,
    isAuthenticated: boolean,
}

interface Properties {
    isAuthenticated: boolean,
    msalInstance: IPublicClientApplication
}

export class SignOutButton extends React.Component<Properties, SignoutState>
{

    constructor(props: any)
    {
        super(props);
        this.state = {
            instance: props.msalInstance,
            isAuthenticated: props.isAuthenticated
        };
    }
    
    handleLogout(instance : any)
    {
        this.state.instance.logoutPopup().catch((e:any) => {
            console.error(e);
        });
    }

    render()
    {
        return <Button variant="secondary" className="ml-auto" onClick={() => this.handleLogout(this.state.instance)}>Sign out</Button>
    }

}