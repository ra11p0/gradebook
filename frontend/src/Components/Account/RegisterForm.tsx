import React, { ReactElement } from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import AccountProxy from '../../ApiClient/Account/AccountProxy';
import Swal from 'sweetalert2';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface RegisterFormProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

interface RegisterFormValues{
    email: string;
    password: string;
    password2: string;
}

const RegisterForm = (props: RegisterFormProps): ReactElement => {
    const {t} = useTranslation();

    const validate = (values: RegisterFormValues)=>{
        const errors: any = {};
        if(values.email.length < 5){
            errors.email = t('emailInvalid')
        }

        if(values.password.length < 5){
            errors.password = t('passwordTooShort')
        }

        if(values.password !== values.password2){
            errors.password = t('passwordsNotTheSame')
        }
        return errors;
    };

    const formik = useFormik({
        initialValues: {
            email:'',
            password:'',
            password2:''
        },
        validate,
        onSubmit: (values: RegisterFormValues)=>{
            AccountProxy.register(values)
                .then(()=>{
                    Swal.fire({
                        title: t('userRegisteredAlertTitle'),
                        text: t('userRegisteredAlertText')
                    });
                });
        }
    });

    return (
        <div className='card m-3 p-3'>
            {
                props.isLoggedIn &&
                <Navigate to="/dashboard"/>
            }
            <div className='card-body'>
                <form onSubmit={formik.handleSubmit}>
                    <div className='m-1 p-1 display-6'>
                        <label>{t('register')}</label>
                    </div>
                    <div className='m-1 p-1'>
                        <label htmlFor='email'>{t('email')}</label>
                        <input className='form-control'
                            id='email'
                            name='email'
                            type='email'
                            onChange={formik.handleChange}
                            value={formik.values.email}
                        />
                        {formik.errors.email ? <div className='invalid-feedback d-block'>{formik.errors.email}</div> : null}
                    </div>
                    <div className='m-1 p-1'>
                        <label htmlFor='password'>{t('password')}</label>
                        <input className='form-control'
                            id='password'
                            name='password'
                            type='password'
                            onChange={formik.handleChange}
                            value={formik.values.password}
                        />
                        {formik.errors.password ? <div className='invalid-feedback d-block'>{formik.errors.password}</div> : null}
                    </div>
                    <div className='m-1 p-1'>
                        <label htmlFor='password2'>{t('confirmPassword')}</label>
                        <input className='form-control'
                            id='password2'
                            name='password2'
                            type='password'
                            onChange={formik.handleChange}
                            value={formik.values.password2}
                        />
                        {formik.errors.password2 ? <div className='invalid-feedback d-block'>{formik.errors.password2}</div> : null}
                    </div>
                    <div className='m-1 p-1 d-flex justify-content-between'>
                        <div className="my-auto d-flex gap-2">
                            <Link to={'/'}>{t('goBackToLoginPage')}</Link>
                        </div>
                        <Button variant='outline-primary' 
                            onClick={()=>props.onLogIn!()} 
                            type='submit'>
                            {t('registerButtonLabel')}
                        </Button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterForm);
