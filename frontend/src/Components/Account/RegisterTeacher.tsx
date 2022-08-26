import React, { ReactElement } from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button, Row } from 'react-bootstrap';
import AccountProxy from '../../ApiClient/Account/AccountProxy';
import Swal from 'sweetalert2';
import CommonNotifications  from '../../Notifications/CommonNotifications'

const mapStateToProps = (state: any) => ({
    
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface RegisterTeacherFormProps{
  
}

interface RegisterTeacherFormValues{
}

const RegisterTeacherForm = (props: RegisterTeacherFormProps): ReactElement => {
    const {t} = useTranslation();

    const validate = (values: RegisterTeacherFormValues)=>{
        const errors: any = {};
        return errors;
    };

    const formik = useFormik({
        initialValues: {
        },
        validate,
        onSubmit: (values: RegisterTeacherFormValues)=>{

        }
    });

    return (
        <div className='card m-3 p-3'>
            <Row className='text-center'>
                <div>
                    Register teacher
                </div>
            </Row>
            <form onSubmit={formik.handleSubmit}>

            </form>
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterTeacherForm);
