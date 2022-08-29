import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button, Row } from 'react-bootstrap';
import RegisterAdministratorPerson, { RegisterAdministratorPersonValues } from './RegisterAdministratorPerson';
import RegisterAdministratorSchool, { RegisterAdministratorSchoolValues } from './RegisterAdministratorSchool';
import AdministratorsProxy from '../../ApiClient/Administrators/AdministratorsProxy';

const mapStateToProps = (state: any) => ({
    
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface RegisterAdministratorFormProps{
    defaultOnBackHandler: ()=>void;
}

const activateWithoutSchool = (person: RegisterAdministratorPersonValues) => {
    AdministratorsProxy.newAdministrator(person).then(e=>console.dir(e));
};

const activateWithSchool = (person: RegisterAdministratorPersonValues, school: RegisterAdministratorSchoolValues) => {
    AdministratorsProxy.newAdministratorWithSchool(person, school).then(e=>console.dir(e));
};

const RegisterAdministratorForm = (props: RegisterAdministratorFormProps): ReactElement => {
    const {t} = useTranslation();
    const [person, setPerson] = useState<RegisterAdministratorPersonValues | null>(null);
    const [school, setSchool] = useState<RegisterAdministratorSchoolValues | null>(null);
    const [showNewSchoolComponent, setShowNewSchoolComponent] = useState(false);

    return (
        <div className='card m-3 p-3'>
            <Button onClick={()=>{
                if(showNewSchoolComponent)
                    setShowNewSchoolComponent(false);
                else
                    props.defaultOnBackHandler();
                }} 
                variant={'link'}>
                {t('back')}
            </Button>
            <Row className='text-center'>
                <div>
                    {t('registerAdministrator')}
                </div>
            </Row>
            <>
              {
                !(showNewSchoolComponent && person) ?
                <RegisterAdministratorPerson
                onSubmit={(values: RegisterAdministratorPersonValues)=>{
                    setPerson(values);
                    setShowNewSchoolComponent(true);
                }}
                name={person?.name}
                surname={person?.surname}
                birthday={person?.birthday}
                /> :
                <>
                    <RegisterAdministratorSchool
                        onSubmit={(values: RegisterAdministratorSchoolValues)=> {
                            setSchool(values);
                            activateWithSchool(person, school!);
                        }}
                        onContinueWithoutSchool={()=>activateWithoutSchool(person)}
                    />
                </>
              }
            </>
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterAdministratorForm);
