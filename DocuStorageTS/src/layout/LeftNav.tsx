import React from 'react'
import { Link } from 'react-router-dom';

export class LeftNav extends React.Component
{
    constructor(props: any) {
        super(props);
        
    }

    render() {
        return <div className="nav-leftside bg-dark">
            <div className="nav-leftitem">
                <Link to="/">Home</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/groups">Show Groups</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/documents">Show Documents</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/backupdocuments">Show Backup Documents</Link>
            </div>
        </div>;
    }

}
