import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { useFormik } from 'formik';
const mapStateToProps = (state: any) => ({ });
const mapDispatchToProps = (dispatch: any) => ({ });
interface RegisterAdministratorPersonProps{
    onSubmit: (values: RegisterAdministratorPersonValues)=>void;
 }
interface RegisterAdministratorPersonValues{

}
const RegisterAdministratorPerson = (props: RegisterAdministratorPersonProps): ReactElement => {
    const {t} = useTranslation();
    const validate = (values: RegisterAdministratorPersonValues)=>{
        const error = {};

        return error;
    };
    const formik = useFormik({
        initialValues: {

        },
        validate,
        onSubmit: props.onSubmit
    });
    return (
        <form onSubmit={formik.handleSubmit}>
            

        </form>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(RegisterAdministratorPerson);
export type {RegisterAdministratorPersonValues};
