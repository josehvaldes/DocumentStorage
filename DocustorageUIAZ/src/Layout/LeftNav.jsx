import { useRecoilValue } from 'recoil';

import { useIsAuthenticated } from "@azure/msal-react";
import { Link } from 'react-router-dom';


function LeftNav() {
    const isAuthenticated = useIsAuthenticated();

    // only show nav when logged in
    if (!isAuthenticated) return null;

    return (
        <div className="nav-leftside bg-dark" >
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
        </div>
    );
}

export { LeftNav };