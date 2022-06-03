import { useEffect, useState } from 'react';
import * as React from 'react';
import { useRecoilValue } from 'recoil';
import { useForm } from "react-hook-form";
import { usersAtom, groupsAtom } from '_state';
import { useUserActions } from '_actions';

export { AssignUserGroup };

function AssignUserGroup()
{
    const users = useRecoilValue(usersAtom);
    const groups = useRecoilValue(groupsAtom);
    const userActions = useUserActions();
    const [selectedUser, setSelectedUser] = useState({
        userId: "-1",
        groups: []
    });

    useEffect(() => {
        userActions.getAll();
        userActions.getAllGroups();
    }, []);

    const { register, handleSubmit, setError, formState } = useForm();
    const { errors, isSubmitting } = formState;

    function onSubmit()
    {
        
        userActions.setGroupsUser({
            userId: selectedUser.userId,
            groups: selectedUser.groups
        });
    }

    function onChangeUser(event)
    {
        var id = event.target.value;
        setSelectedUser({
            userId: id,
            groups: []
        });

        if (id > 0) {
            userActions.getGroupsByUser(id).then(result =>{

                var list = [];
                result.forEach(item => {
                    list.push(item.id);
                });
                
                setSelectedUser({
                    userId: id,
                    groups: list
                });
            });
        }
    }

    function changeCheckbox(event)
    {
        var id = parseInt(event.target.value);
        var selectedCheckboxes = selectedUser.groups;
        const findIdx = selectedCheckboxes.indexOf(id);
        if (findIdx > -1) {
            selectedCheckboxes.splice(findIdx, 1);
        } else {
            selectedCheckboxes.push(id);
        }

        setSelectedUser({
            userId: selectedUser.userId,
            groups: selectedCheckboxes
        });
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h2>Assign Groups to User: {selectedUser.userid}</h2>

            <form onSubmit={handleSubmit(onSubmit)}>

                <div className="form-group">
                    <label>Select the User </label>
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

                <h3>Groups</h3>


                <div className="form-group ">
                    <label>Mark the Documents you want to assign</label>
                    {groups &&
                        <div className="checkboxGroup">
                        {groups.map(group =>
                            <div key={group.id}>
                                <input type="checkbox" value={group.id}
                                    checked={selectedUser.groups.includes(group.id)} onChange={changeCheckbox} />
                                <label className="checkboxLabel" key={group.id} >{group.name}</label>
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