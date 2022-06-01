import { useEffect } from 'react';
import * as React from 'react';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { useUserActions } from '_actions';

export { EditGroup };

function EditGroup() {
    const userActions = useUserActions();

    useEffect(() => {
    }, []);

    // form validation rules 
    const validationSchema = Yup.object().shape({
        name: Yup.string().required('Name is required'),
    });
    const formOptions = { resolver: yupResolver(validationSchema) };

    const { register, handleSubmit, setError, formState } = useForm(formOptions);
    const { errors, isSubmitting } = formState;


    function onSubmit({ name }) {
        return userActions.createGroup({
            name: name,
        }).catch(error => {
                setError('apiError', { message: error });
            });
    }


    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h1>Add Group</h1>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label>Name</label>
                    <input name="username" type="text" {...register('name')} className={`form-control ${errors.name ? 'is-invalid' : ''}`} />
                    <div className="invalid-feedback">{errors.name?.message}</div>
                </div>

                <button disabled={isSubmitting} className="btn btn-primary float-right">
                    {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                    Submit
                </button>
            </form>
        </div>
   );
}