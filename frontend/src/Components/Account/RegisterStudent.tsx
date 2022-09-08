import React, { ReactElement, useState } from 'react';
import { connect } from 'react-redux';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button, Col, Row } from 'react-bootstrap';
import InvitationsProxy from '../../ApiClient/Invitations/InvitationsProxy';
import moment from 'moment';


const mapStateToProps = (state: any) => ({

});

const mapDispatchToProps = (dispatch: any) => ({

});

interface RegisterStudentFormProps {
    defaultOnBackHandler: () => void;
}

interface RegisterStudentFormValues {
    accessCode: string;
}

const RegisterStudentForm = (props: RegisterStudentFormProps): ReactElement => {
    const { t } = useTranslation('registerStudent');

    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [birthday, setBirthday] = useState('');
    const [_class, setClass] = useState('');
    const [group, setGroup] = useState('');

    const validate = (values: RegisterStudentFormValues) => {
        const errors: any = {};
        if (values.accessCode.length != 6) {
            errors.accessCode = 'wrong access code length';
        }
        return errors;
    };

    const formik = useFormik({
        initialValues: {
            accessCode: ''
        },
        validate,
        onSubmit: (values: RegisterStudentFormValues) => {

        }
    });

    const handleAccessCodeChange = function (e: any) {
        if (e.target.value.length == 6) {
            InvitationsProxy.getInvitationDetailsForStudent(e.target.value)
                .then(resp => {
                    var data = resp.data;
                    setName(data.person.name);
                    setSurname(data.person.surname);
                    setBirthday(moment(data.person.birthday).format('YYYY-MM-DD'));
                    setClass(data.class?.name);
                    setGroup(data.group?.name);
                })
                .catch(err => {
                    setName('');
                    setSurname('');
                    setBirthday('');
                    setClass('');
                    setGroup('');
                });
        }
    };

    return (
        <div className='card m-3 p-3'>
            <Button onClick={props.defaultOnBackHandler} variant={'link'}>
                {t('back')}
            </Button>
            <Row className='text-center'>
                <div className='h4'>
                    {t('registerStudent')}
                </div>
            </Row>
            <form onSubmit={formik.handleSubmit}>
                <div className='m-1 p-1'>
                    <label htmlFor='accessCode'>{t('accessCode')}</label>
                    <input className='form-control'
                        id='accessCode'
                        name='accessCode'
                        type='text'
                        onChange={formik.handleChange}
                        value={formik.values.accessCode}
                        onInput={handleAccessCodeChange}

                    />
                    {formik.errors.accessCode && formik.touched.accessCode ? <div className='invalid-feedback d-block'>{formik.errors.accessCode}</div> : null}
                </div>
                <Row>
                    <Col>
                        <div className='m-1 p-1'>
                            <label>{t('name')}</label>
                            <input className='form-control'
                                type='text'
                                defaultValue={name}
                                disabled
                            />
                        </div>
                    </Col>
                    <Col>
                        <div className='m-1 p-1'>
                            <label>{t('surname')}</label>
                            <input className='form-control'
                                type='text'
                                defaultValue={surname}
                                disabled
                            />
                        </div>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <div className='m-1 p-1'>
                            <label>{t('birthday')}</label>
                            <input className='form-control'
                                type='text'
                                defaultValue={birthday}
                                disabled
                            />
                        </div>
                    </Col>
                    <Col>
                        <div className='m-1 p-1'>
                            <label>{t('class')}</label>
                            <input className='form-control'
                                type='text'
                                defaultValue={_class}
                                disabled
                            />
                        </div>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <div className='m-1 p-1'>
                            <label>{t('group')}</label>
                            <input className='form-control'
                                type='text'
                                defaultValue={group}
                                disabled
                            />
                        </div>
                    </Col>
                </Row>


                <Button variant='outline-primary'
                    type='submit'>
                    {t('confirmInformation')}
                </Button>
            </form>
        </div>
    );
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterStudentForm);
