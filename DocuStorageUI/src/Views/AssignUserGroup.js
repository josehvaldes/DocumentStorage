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
    const [selectedUser, setSelectedUser] = useState(false);

    useEffect(() => {
        userActions.getAll();
        userActions.getAllGroups();
    }, []);

    const { register, handleSubmit, setError, formState } = useForm();
    const { errors, isSubmitting } = formState;

    function onSubmit()
    {
        var userId = selectedUser;
        var list = []
        document.querySelectorAll('input[name=docscheck]:checked').forEach(e => list.push(e.value));
        userActions.setGroupsUser({
            userId: userId,
            groups: list
        });


    }

    function onChangeUser(event)
    {
        var id = event.target.value;
        setSelectedUser(id)
        if (id > 0) {
            userActions.getGroupsByUser(id).then(result =>{
                groups.forEach(group => {
                    var element = document.getElementById("c_" + group.id);
                    element.checked = false;
                    result.forEach(item => {
                        if (group.id == item.id) {
                            element.checked = true;
                        }
                    });
                });

            });

        }
        else {
            groups.forEach(group => {
                document.getElementById("c_" + group.id).checked = false;
            });
        }
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h2>Assign Groups to User</h2>

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
                                <input type="checkbox" id={"c_" + group.id} name="docscheck" defaultValue={group.id} />
                                <label className="checkboxLabel" key={group.id}>{group.name}</label>
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