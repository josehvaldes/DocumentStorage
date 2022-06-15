import React, { Component } from 'react'

import { FetchCategories, FetchBackupDocuments } from '../_helpers/FetchWrapper';

interface BackupParams {
    documents?: [],
    authInfo?: any
}

interface BackupState {
    documents: any,
    categories: any,
    authInfo?: any,
    selectedCategory:string
}


export class BackupDocuments extends Component<BackupParams, BackupState>{

    constructor(props: any) {
        super(props);
        this.state = {
            documents: null,
            categories: null,
            authInfo: props.authInfo,
            selectedCategory:"",
        };

        this.onChange = this.onChange.bind(this);
        this.onQuery = this.onQuery.bind(this);
    }

    onChange(event:any) {
        var value = event.target.value;

        this.setState({
            ...this.state,
            selectedCategory: value
        });
    }

    onQuery() {
        if (this.state.selectedCategory!="" && this.state.selectedCategory != "0") {
            this.setState({
                ...this.state,
                documents: []
            });

            FetchBackupDocuments(this.props.authInfo, this.state.selectedCategory).then((response: any) => {
                this.setState({
                    ...this.state,
                    documents: response
                });
            });
        }
        else {
            //nothing to do
            this.setState({
                ...this.state,
                documents: []
            });
        }
    }

    componentDidMount() {
        this.reloadCategories();
    }

    componentDidUpdate(previousProps: any, previousState: any) {
        if (previousProps.authInfo !== this.props.authInfo) {
            this.reloadCategories();
        }
    }

    reloadCategories()
    {
        FetchCategories(this.props.authInfo).then((response: any) => {
            this.setState({
                ...this.state,
                categories: response
            });
        });
    }

    render() {

        return (
            <div className="content">
                <h4>Categories available</h4>
                {this.state.categories && this.state.categories.length > 0 &&
                    <div>
                        <select className="form-control" name="user" id="user"
                            onChange={this.onChange} >
                            <option value="0">Select on User </option>
                            {this.state.categories.map((cat:any) =>
                                <option value={cat} id={cat} key={cat} > {cat}</option>
                            )}
                        </select>
                        <div className="content-right">
                            <button className="btn btn-primary" onClick={this.onQuery}>
                                Search documents
                            </button>
                        </div>
                    </div>
                }
                
                {this.state.documents && this.state.documents.length > 0 &&
                    <div className="form-group ">
                        <h4>Documents in the backup</h4>
                        <table className="documentable table table-striped">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Category</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.documents.map((doc:any) =>
                                    <tr key={doc.id}>
                                        <td>{doc.name}</td>
                                        <td>{doc.category}</td>
                                        <td>{doc.description}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                }
            </div>
        );

    }
}
