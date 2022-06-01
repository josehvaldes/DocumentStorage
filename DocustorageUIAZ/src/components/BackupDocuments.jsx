import React, { useEffect, useState } from "react";
import { documentsAtom, categoriesAtom, backupdocumentsAtom } from '../_state';
import { useUserActions } from '../_actions/';
import { useRecoilValue, useRecoilState } from 'recoil';

export { BackupDocuments };

function BackupDocuments() {

    const userActions = useUserActions();
    const categories = useRecoilValue(categoriesAtom);
    const [documents, setBackupDocuments] = useRecoilState(backupdocumentsAtom);

    const [category, setCategory] = useState(false);

    useEffect(() => {
        userActions.getCategories();
    }, []);


    function onChange(event)
    {
        var value = event.target.value;
        setCategory(value);
    }

    function onQuery()
    {
        setBackupDocuments(null);
        userActions.getBackupDocuments(category);
        setCategory(false);
    }

    return (
        <div>
            <label>Categories available</label>
            {categories && categories.length>0 &&
                <center>
                
                <select className="form-control" name="user" id="user"
                    onChange={onChange} >
                    <option value="0">Select on User </option>
                    {categories.map(cat =>
                        <option value={cat} id={cat} key={cat} > {cat}</option>
                    )}
                </select>
                </center>
            }
            <button className="btn btn-primary" onClick={ onQuery }>
                Search documents 
            </button>

            {documents && documents.length > 0 &&
                <div className="form-group ">
                    <h4>Documents in the backup</h4>
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
                        {documents.map(doc =>
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
