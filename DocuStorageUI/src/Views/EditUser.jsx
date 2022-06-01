import { useEffect } from 'react';
import * as React from 'react';
import { useNavigate } from "react-router-dom";
import { useRecoilValue } from 'recoil';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { authAtom, usersAtom } from '_state';
import { useUserActions } from '_actions';

export { EditUser };

function EditUser() {
    const userActions = useUserActions();

    //Don't change the values
    const Roles = [
        { value: 4, label: 'User' },
        { value: 2, label: 'Manager' },
        { value: 1, label: 'Admin' }
    ];

    useEffect(() => {
    }, []);

    // form validation rules 
    const validationSchema = Yup.object().shape({
        username: Yup.string().required('Username is required'),
        password: Yup.string().required('Password is required'),
        retryPassword: Yup.string().required('Retry Password is required')
    });

    const formOptions = { resolver: yupResolver(validationSchema) };

    const { register, handleSubmit, setError, formState } = useForm(formOptions);
    const { errors, isSubmitting } = formState;

    function onSubmit({ username, password, retryPassword, role })
    {
        if (password != retryPassword) {
            setError('apiError', { message: "Password mismatch" });
            return;
        }
        return userActions.createUser({
            username: username,
            password: password,
            role: role
        })
            .catch(error => {
                setError('apiError', { message: error });
            });
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h2>Create User</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label>Username </label><label className="required">*</label>
                    <input name="username" type="text" {...register('username')} className={`form-control ${errors.username ? 'is-invalid' : ''}`}/>
                    <div className="invalid-feedback">{errors.username?.message}</div>
                </div>

                <div className="form-group">
                    <label>Password </label><label className="required">*</label>
                    <input name="password" type="password" {...register('password')} className={`form-control ${errors.password ? 'is-invalid' : ''}`}/>
                    <div className="invalid-feedback">{errors.password?.message}</div>
                </div>

                <div className="form-group">
                    <label>Repeat Password </label><label className="required">*</label>
                    <input name="retryPassword" type="password" {...register('retryPassword')} className={`form-control ${errors.retryPassword ? 'is-invalid' : ''}`}/>
                    <div className="invalid-feedback">{errors.retryPassword?.message}</div>
                </div>

                <div className="form-group">
                    <label>Role</label>
                    <select className="form-control" name="Role" {...register('role')}>
                        {Roles.map((option) => (
                            <option value={option.value} key={option.value}> {option.label} </option>
                        ))}
                    </select>
                </div>
                {errors.apiError &&
                    <div className="alert alert-danger mt-3 mb-0">{errors.apiError?.message}</div>
                }
                <div>
                    <button disabled={isSubmitting} className="btn btn-primary float-right">
                        {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                        Submit
                    </button>
                </div>
                
            </form>
        </div>
    );
}
