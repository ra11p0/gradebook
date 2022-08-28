import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button, Row } from 'react-bootstrap';
import RegisterAdministratorPerson, { RegisterAdministratorPersonValues } from './RegisterAdministratorPerson';
import RegisterAdministratorSchool, { RegisterAdministratorSchoolValues } from './RegisterAdministratorSchool';

const mapStateToProps = (state: any) => ({
    
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface RegisterAdministratorFormProps{
  
}

const RegisterAdministratorForm = (props: RegisterAdministratorFormProps): ReactElement => {
    const {t} = useTranslation();
    const [state, setState] = useState({
        person: null,
        school: null
    });


    return (
        <div className='card m-3 p-3'>
            <Row className='text-center'>
                <div>
                    {t('registerAdministrator')}
                </div>
            </Row>
            <>
              {
                state.person ?
                <RegisterAdministratorSchool
                    onSubmit={(values: RegisterAdministratorSchoolValues)=>{}}
                />: 
                <RegisterAdministratorPerson
                    onSubmit={(values: RegisterAdministratorPersonValues)=>{}}
                /> 
              }
            </>
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterAdministratorForm);
