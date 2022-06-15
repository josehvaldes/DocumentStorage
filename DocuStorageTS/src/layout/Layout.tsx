import React from 'react'
import { Navbar } from 'react-bootstrap';
import { SignOutButton } from './SignOutButton';
import { SignInButton } from './SignInButton';
import { IPublicClientApplication } from "@azure/msal-browser";

interface PropsP {
    //children: JSX.Element[] | JSX.Element
    children: React.ReactNode,
    isAuthenticated: boolean,
    msalinstance: IPublicClientApplication
}

interface StateLayout
{
    isAuthenticated: boolean,
    msalinstance: IPublicClientApplication
}

export class Layout extends React.Component<PropsP, StateLayout>
{

    constructor(props: PropsP)
    {
        super(props);

        this.state = {
            isAuthenticated: props.isAuthenticated,
            msalinstance: props.msalinstance
        };       
    }    
    componentDidUpdate(previousProps: any, previousState: any) {
        if (previousProps.isAuthenticated !== this.props.isAuthenticated) {
            this.setState({
                isAuthenticated: this.props.isAuthenticated,
                msalinstance: this.props.msalinstance
            });
        }
    }

    render()
    {

        return <div className="container">
            <Navbar bg="dark" variant="dark">
                <a className="navbar-brand" href="/">Document Storage Azure AD</a>
                {this.state.isAuthenticated ?
                    <SignOutButton isAuthenticated={this.state.isAuthenticated} msalInstance={this.state.msalinstance} /> :
                    <SignInButton msalInstance={this.state.msalinstance} isAuthenticated={this.state.isAuthenticated} />}
            </Navbar>
            <div>{this.props.children}</div>
        </div>;
    }

}
