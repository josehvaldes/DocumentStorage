
import { useEffect } from 'react';
import * as React from 'react';
import { useRecoilValue } from 'recoil';
import { useForm } from "react-hook-form";
import { documentsAtom, groupsAtom } from '_state';
import { useUserActions } from '_actions';

export { AssignDocsGroups };

function AssignDocsGroups()
{
    const documents = useRecoilValue(documentsAtom);
    const groups = useRecoilValue(groupsAtom);
    const userActions = useUserActions();

    useEffect(() => {
        userActions.getAllGroups();
        userActions.getAllDocuments();
    }, []);

    const { register, handleSubmit, setError, formState } = useForm();
    const { errors, isSubmitting } = formState;

    function onSubmit()
    {
        var e = document.getElementById("group");
        var groupId = e.options[e.selectedIndex].value;
        var list = []
        document.querySelectorAll('input[name=docscheck]:checked').forEach(e => list.push(e.value));
        userActions.setGroupDocuments({
            groupId: groupId,
            documents: list
        });
    }

    function onChangeGroup(event)
    {
        var id = event.target.value;
        if (id > 0) {
            userActions.getDocumentsByGroup(id).then(result => {
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
        else {
            documents.forEach(doc => {
                document.getElementById("c_" + doc.id).checked = false;
            });
        }
    }


    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h2>Assign Documents to Groups</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label>Select a Group </label>
                    {groups &&
                        <select className="form-control" name="group" id="group"
                        onChange={onChangeGroup} >
                            <option value="0">Select on Group </option>
                            {groups.map(group =>
                                <option value={group.id} id={group.id} key={group.id} >{group.name}</option>
                            )}
                        </select>
                    }
                    {!groups && <div className="spinner-border spinner-border-sm"></div>}
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