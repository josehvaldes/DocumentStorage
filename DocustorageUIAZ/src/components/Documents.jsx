import React, { useEffect} from "react";
import { documentsAtom } from '../_state';
import { useUserActions } from '../_actions/';
import { useRecoilValue } from 'recoil';

export { Documents };

function Documents () {

    const userActions = useUserActions();
    const documents = useRecoilValue(documentsAtom);

    useEffect(() => {
        userActions.getAllDocuments();
    }, []);

    return (
        <div>
            {documents && documents.length > 0 &&
                <center>
                    <table className="documentable table table-striped">
                        <thead>
                            <tr>
                                <th>NAME</th>
                                <th>CATEGORY</th>
                                <th>DESCRIPTION</th>
                            </tr>
                        </thead>
                        <tbody>
                        {documents.map(doc =>
                            <tr key={doc.id}>
                                <td>{doc.name}</td>
                                <td>{doc.category}</td>
                                <td>{doc.description}</td>
                            </tr>
                            )}
                        </tbody>
                    </table>
                </center>
            }
        </div>
    );
}