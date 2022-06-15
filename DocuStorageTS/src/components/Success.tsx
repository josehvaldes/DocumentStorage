
import React, { Component } from 'react'


interface SuccessProps {
    title: string
}

interface SuccessState {
    title: string
}

export class Success extends Component<SuccessProps, SuccessState>
{

    constructor(props: SuccessProps) {
        super(props);
        this.state = {
            title: props.title
        };
    }

    render() {
        return <div className="home content">
            <h2>{this.state.title}</h2>
        </div>
    }

}