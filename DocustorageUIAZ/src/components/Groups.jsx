import React, { useEffect } from "react";
import { useUserActions } from '../_actions/';
import { groupsAtom } from '../_state';
import { useRecoilValue } from 'recoil';

export { Groups };

function Groups() {

    const userActions = useUserActions();
    const groups = useRecoilValue(groupsAtom);

    useEffect(() => {
        userActions.getAllGroups();
    }, []);

    return (
        <div>
            {groups && groups.length>0 && 
            <center>
                <table className="documentable table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>NAME</th>
                        </tr>
                    </thead>
                    <tbody>
                        {groups.map(gr =>
                            <tr key={gr.id}>
                                <td>{gr.id}</td>
                                <td>{gr.name}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </center>
            }
        </div>
        );

}