import { useEffect } from 'react';
import * as React from 'react';
import { useNavigate } from "react-router-dom";
import { useRecoilValue } from 'recoil';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { authAtom, usersAtom } from '_state';
import { useUserActions } from '_actions';


export { UploadDocument };

function UploadDocument() {
    const auth = useRecoilValue(authAtom);
    const users = useRecoilValue(usersAtom);
    const userActions = useUserActions();

    useEffect(() => {
    }, []);

    // form validation rules 
    const validationSchema = Yup.object().shape({
        name: Yup.string().required('name is required')
    });

    const formOptions = { resolver: yupResolver(validationSchema) };

    const { register, handleSubmit, setError, formState } = useForm(formOptions);
    const { errors, isSubmitting } = formState;

    function onSubmit({ name, category, description, file })
    {
        userActions.uploadDocument({
            name: name,
            category: category,
            description: description,
            files: file
        }).catch(error => {
            setError('apiError', { message: error });
        });
    }

    return (
        <div className="containerBody col-md-6 offset-md-3">
            <h1>Upload document</h1>

            <form onSubmit={handleSubmit(onSubmit)} encType="multipart/form-data">

                <div className="form-group">
                    <label>Name *</label>
                    <input name="name" type="text" {...register('name')} className={`form-control ${errors.username ? 'is-invalid' : ''}`} />
                    <div className="invalid-feedback">{errors.name?.message}</div>
                </div>

                <div className="form-group">
                    <label>Category *</label>
                    <input name="category" type="text" {...register('category')} className={`form-control ${errors.category ? 'is-invalid' : ''}`} />
                </div>

                <div className="form-group">
                    <label>Description</label>
                    <div>
                        <textarea defaultValue="" name="description" rows="4" cols="50" {...register('description')} className={`form-control ${errors.description ? 'is-invalid' : ''}`}>
                        </textarea>
                     </div>
                </div>

                <div className="form-group">
                    <label>Upload file *</label>
                    <input name="file" type="file" {...register('file')} className={`form-control ${errors.file ? 'is-invalid' : ''}`} />
                </div>

                <button disabled={isSubmitting} className="btn btn-primary float-right">
                    {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                    Submit
                </button>
            </form>
        </div>
    );
}
