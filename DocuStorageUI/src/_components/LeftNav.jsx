import { useRecoilValue } from 'recoil';

import { authAtom } from '_state';
import { useUserActions } from '_actions';
import { Link } from 'react-router-dom';


function LeftNav()
{
    const auth = useRecoilValue(authAtom);

    // only show nav when logged in
    if (!auth) return null;

    return (
        <div className="nav-leftside">
            <div className="nav-leftitem">
                <Link to="/AddUser">Add User</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/UploadDocument">Upload Document</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/ListDocuments">List Your Document</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/AddGroup">Add Group</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/AssignDocsUsers">Assign Docs to Users</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/AssignDocsGroups">Assign Docs to Groups</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/AssignUserGroup">Assign User to Groups</Link>
            </div>
            <div className="nav-leftitem">
                <Link to="/DeleteDocuments">Delete Documents</Link>
            </div>
        </div>
        );
}

export { LeftNav };