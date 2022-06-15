import React, { Component } from 'react'

import { FetchGroups} from '../_helpers/FetchWrapper';

interface GroupParams {
    groups?: [],
    authInfo?: any
}

interface GroupState
{
    groups: any,
    authInfo?: any
}

export class Groups extends Component<GroupParams, GroupState>
{

    constructor(props: any) {
        super(props);
        this.state = {
            groups: null,
            authInfo: props.authInfo
        };
    }


    componentDidMount() {
        this.reloadGroups();
    }

    componentDidUpdate(previousProps: any, previousState: any) {
        if (previousProps.authInfo !== this.props.authInfo) {
            this.reloadGroups();
        }
    }

    reloadGroups()
    {
        FetchGroups(this.props.authInfo).then((response: any) => {
            this.setState({
                ...this.state,
                groups: response
            });
        });
    }

    render() {
        return <div className="">
            <h2>Groups: </h2>
            {this.state.groups && this.state.groups.length > 0 &&
                <table className="documentable table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>NAME</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.groups.map((gr:any) =>
                            <tr key={gr.id}>
                                <td>{gr.id}</td>
                                <td>{gr.name}</td>
                            </tr>
                        )}
                    </tbody>
                </table>

            }


        </div>
    }

}