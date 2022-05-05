import { useEffect } from 'react';
import * as React from 'react';
import { useNavigate } from "react-router-dom";
import { useRecoilValue } from 'recoil';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { authAtom, documentsByUserAtom, documentsInGroupsAtom } from '_state';
import { useUserActions } from '_actions';

export { ListDocuments };

function ListDocuments()
{
    const auth = useRecoilValue(authAtom);
    const userDocs = useRecoilValue(documentsByUserAtom);
    const groupDocs = useRecoilValue(documentsInGroupsAtom);
    const userActions = useUserActions();

    useEffect(() => {
        userActions.getAll();
        userActions.getAllDocuments();

        userActions.getDocumentsByUser(auth?.id);
        userActions.getDocumentsInGroupsByUser(auth?.id);

    }, []);

    function download(event)
    {
        event.preventDefault();
        var id = event.target.id;
        var name = event.target.name;
        userActions.downloadDocument(id, name);
    }


    return (
        <div className="containerBody col-md-6 offset-md">
            <h2>Your assigned documents</h2>
            {userDocs && userDocs.length > 0 &&
                <div className="form-group ">
                    <h4>Documents assigned to User</h4>
                <table className="documentable table table-striped">
                    <thead>
                        <tr>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Description</th>
                                <th></th>
                        </tr>
                    </thead>
                    <tbody>
                            {userDocs.map(doc =>
                                <tr key={doc.id}>
                                    <td>{doc.name}</td>
                                    <td>{doc.category}</td>
                                    <td>{doc.description}</td>
                                    <td><a href="#" id={doc.id} name={doc.name} onClick={download}>Download</a></td>
                                </tr>
                            )}
                    </tbody>
                </table>
                </div>
            }

            {groupDocs && groupDocs.length > 0 &&
                <div className="form-group ">
                    <h4>Documents assigned to Groups</h4>
                <table className="documentable table table-striped">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Category</th>
                            <th>Description</th>
                            <th>Group</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                            {groupDocs.map(doc =>
                                <tr key={doc.id}>
                                    <td>{doc.name}</td>
                                    <td>{doc.category}</td>
                                    <td>{doc.description}</td>
                                    <td>{doc.source}</td>
                                    <td><a href="#" id={doc.id} name={doc.name} onClick={download}> Download</a></td>
                                </tr>
                        )}
                    </tbody>
                </table>
                </div>
            }
            


        </div>
    );
}