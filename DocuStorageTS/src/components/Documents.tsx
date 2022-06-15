import React, { Component } from 'react'

import { FetchDocuments } from '../_helpers/FetchWrapper';


interface DocumentParams {
    documents?: [],
    authInfo?: any
}

interface DocumentState {
    documents: any,
    authInfo?: any
}

export class Documents extends Component<DocumentParams, DocumentState>{

    constructor(props: any) {
        super(props);
        this.state = {
            documents: null,
            authInfo: props.authInfo
        };
    }


    componentDidMount() {
        this.reloadDocuments();
    }

    componentDidUpdate(previousProps: any, previousState: any) {
        if (previousProps.authInfo !== this.props.authInfo) {
            this.reloadDocuments();
        }
    }

    reloadDocuments(){
        FetchDocuments(this.props.authInfo).then((response: any) => {
            this.setState({
                ...this.state,
                documents: response
            });
        });
    }


    render() {
        return <div className="home content">
            <h2>Documents </h2>
            {this.state.documents && this.state.documents.length > 0 &&
                <table className="documentable table table-striped">
                    <thead>
                        <tr>
                            <th>NAME</th>
                            <th>CATEGORY</th>
                            <th>DESCRIPTION</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.documents.map((doc: any) =>
                            <tr key={doc.id}>
                                <td>{doc.name}</td>
                                <td>{doc.category}</td>
                                <td>{doc.description}</td>
                            </tr>
                        )}
                    </tbody>
                </table>

            }


        </div>
    }

}