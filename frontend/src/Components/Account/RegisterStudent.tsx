import React, { ReactElement } from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button } from 'react-bootstrap';
import AccountProxy from '../../ApiClient/Account/AccountProxy';
import Swal from 'sweetalert2';
import CommonNotifications  from '../../Notifications/CommonNotifications'

const mapStateToProps = (state: any) => ({
    
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface RegisterStudentFormProps{
    goBackHandler: ()=>void
}

interface RegisterStudentFormValues{

}

const RegisterStudentForm = (props: RegisterStudentFormProps): ReactElement => {
    const {t} = useTranslation();

    const validate = (values: RegisterStudentFormValues)=>{
        const errors: any = {};
        return errors;
    };

    const formik = useFormik({
        initialValues: {
            
        },
        validate,
        onSubmit: (values: RegisterStudentFormValues)=>{
           
        }
    });

    return (
        <div className='card m-3 p-3'>
            <Button onClick={props.goBackHandler}>
                go back
            </Button>
            register student
                
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterStudentForm);
