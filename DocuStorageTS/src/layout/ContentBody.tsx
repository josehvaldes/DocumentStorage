import React from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Home } from '../components/Home';
import { Groups } from '../components/Groups';
import { BackupDocuments } from '../components/BackupDocuments';
import { Documents } from '../components/Documents';
import { Success } from '../components/Success';
import { LeftNav } from './LeftNav';
import { PrivateRoute } from './PrivateRoute';
import { AccountInfo } from "@azure/msal-browser";

interface Properties{
    accountInfo: AccountInfo,
    authInfo?: any
}

interface ContentState {
    accountInfo: AccountInfo,
    authInfo?: any
}

export class ContentBody extends React.Component<Properties, ContentState>
{
    constructor(props: Properties)
    {
        super(props);
        this.state = {
            accountInfo: props.accountInfo,
            authInfo: props.authInfo
        };
    }

    componentDidUpdate(previousProps: any, previousState: any)
    {
        if (previousProps.authInfo !== this.props.authInfo) {
            this.setState({
                ...this.state,
                authInfo: this.props.authInfo
            });
        }
    }



    render()
    {
        return <div>
            <BrowserRouter>
                <LeftNav />
                <div className="bodycontainer pt-4 pb-4">
                    <Routes>
                        <Route path="/" element={
                            <PrivateRoute><Home accountInfo={this.state.accountInfo} /></PrivateRoute >
                        } />
                        <Route path="/" element={<Home accountInfo={this.props.accountInfo} />} />
                        <Route path="/groups" element={<Groups authInfo={this.state.authInfo} />} />
                        <Route path="/documents" element={<Documents authInfo={this.props.authInfo} />} />
                        <Route path="/backupdocuments" element={<BackupDocuments authInfo={this.props.authInfo} />} />
                        <Route path="/success" element={<Success title="success" />} />
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    }
}