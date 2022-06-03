
import { useEffect, useState } from 'react';
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
    const [selectedGroup, setSelectedGroup] = useState({
        groupId: "-1",
        docs: []
    });

    useEffect(() => {
        userActions.getAllGroups();
        userActions.getAllDocuments();
    }, []);

    const { register, handleSubmit, setError, formState } = useForm();
    const { errors, isSubmitting } = formState;

    function onSubmit()
    {
        var groupId = selectedGroup.groupId;
        var list = selectedGroup.docs;
        
        userActions.setGroupDocuments({
            groupId: groupId,
            documents: list
        });
    }

    function onChangeGroup(event)
    {
        var id = event.target.value;
        setSelectedGroup({
            groupId: id,
            docs: []
        });

        if (id > 0) {
            userActions.getDocumentsByGroup(id).then(result => {

                var list = [];
                result.forEach(item => {
                    list.push(item.id);
                });

                setSelectedGroup({
                    groupId: id,
                    docs: list
                });
            });
        }
    }

    function changeCheckbox(event)
    {
        var id = parseInt(event.target.value);
        var selectedCheckboxes = selectedGroup.docs;
        const findIdx = selectedCheckboxes.indexOf(id);
        if (findIdx > -1) {
            selectedCheckboxes.splice(findIdx, 1);
        } else {
            selectedCheckboxes.push(id);
        }

        setSelectedGroup({
            groupId: selectedGroup.groupId,
            docs: selectedCheckboxes
        });
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h3>Assign Documents to Groups</h3>
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
                                    <input type="checkbox" value={doc.id}
                                        checked={selectedGroup.docs.includes(doc.id)} onChange={changeCheckbox} />
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