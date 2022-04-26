
import { useEffect } from 'react';
import * as React from 'react';
import { useRecoilValue, useRecoilState } from 'recoil';
import { useForm } from "react-hook-form";
import { usersAtom, documentsAtom } from '_state';
import { useUserActions } from '_actions';


export { DeleteDocuments }

function DeleteDocuments()
{
    var documents = useRecoilValue(documentsAtom);

    const [docs, setDocs] = useRecoilState(documentsAtom);

    const userActions = useUserActions();


    useEffect(() => {
        userActions.getAllDocuments();
    }, []);

    function deleteDocument(event) {
        event.preventDefault();
        var id = event.target.id;
        var name = event.target.name;
        userActions.deleteDocument(id, name).then(result =>
        {
            var newDocs = docs.filter(item => item.id !== parseInt(id));
            setDocs(newDocs)
        });
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h3>Delete Documents</h3>
            {documents && documents.length > 0 &&
                <div className="form-group ">
                    <h4>Documents assigned to Groups</h4>
                <table className="documentable">
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
                        {documents.map(doc =>
                            <tr key={doc.id} id={"row_"+doc.id}>
                                <td>{doc.name}</td>
                                <td>{doc.category}</td>
                                <td>{doc.description}</td>
                                <td>
                                    <a href="#" id={doc.id} name={doc.name} onClick={deleteDocument}> Delete</a>
                                </td>
                            </tr>
                        )}
                    </tbody>
                    </table>

                </div>
            }

        </div>
        );

}