import { useEffect, useState } from 'react';
import * as React from 'react';
import { useRecoilValue} from 'recoil';
import { useForm } from "react-hook-form";
import { usersAtom, documentsAtom } from '_state';
import { useUserActions } from '_actions';

export { AssignDocsUsers };

function AssignDocsUsers()
{
    const users = useRecoilValue(usersAtom);
    var documents = useRecoilValue(documentsAtom);

    const [selectedUser, setSelectedUser] = useState(false);

    const userActions = useUserActions();

    useEffect(() => {
        userActions.getAll();
        userActions.getAllDocuments();
    }, []);


    const { register, handleSubmit, setError, formState } = useForm();
    const { errors, isSubmitting } = formState;

    function onSubmit()
    {
        var userId = selectedUser;
        var list = []
        document.querySelectorAll('input[name=docscheck]:checked').forEach(e => list.push(e.value));
        userActions.setUserDocuments({
            userId: userId,
            documents: list
        });
    }

    function onChangeUser(event)
    {
        var id = event.target.value;
        setSelectedUser(id)

        if (id > 0) {
            userActions.getDocumentsByUser(id).then(result => {
                documents.forEach(doc => {
                    var element = document.getElementById("c_" + doc.id);
                    element.checked = false;
                    result.forEach(item => {
                        if (doc.id == item.id) {
                            element.checked = true;
                        }
                    });
                });
            });
        }
        else
        {
            documents.forEach(doc => {
                document.getElementById("c_" + doc.id).checked = false;
            });
        }
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h2>Assign Documents to Users</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label>Select a User </label>
                    {users &&
                        <select className="form-control" name="user" id="user"
                                onChange={onChangeUser} >
                            <option value="0">Select on User </option>
                            {users.map(user =>
                                <option value={user.id} id={user.id} key={user.id} > {user.username}</option>
                            )}
                        </select>
                    }
                    {!users && <div className="spinner-border spinner-border-sm"></div>}
                </div>
                
                <h3>Documents</h3>

                <div className="form-group ">
                    <label>Mark the Documents you want to assign</label>
                    {documents &&
                        <div className="checkboxGroup">
                            {documents.map(doc =>
                                <div key={doc.id}>
                                    <input type="checkbox" id={"c_" + doc.id} name="docscheck" defaultValue={doc.id} />
                                    <label className="checkboxLabel" key={doc.id}>{doc.name}</label>
                                </div>
                            )}
                        </div>
                    }
                
                </div>

                <button disabled={isSubmitting} className="btn btn-primary float-right">
                    {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                    Submit
                </button>
            </form>
        </div>
        );
}