
import React, { Component } from 'react'
import { AccountInfo } from "@azure/msal-browser";

interface HomeParams
{
    accountInfo: AccountInfo
}

interface HomeState
{
    accountInfo: AccountInfo,
}

export class Home extends Component<HomeParams, HomeState>
{

    constructor(props: HomeParams)
    {
        super(props);

        this.state = {
            accountInfo: props.accountInfo
        };
    }

    render()
    {
        return <div className="home content">
            <h2>Welcome {this.state.accountInfo.name}</h2>
        </div>
    }

}