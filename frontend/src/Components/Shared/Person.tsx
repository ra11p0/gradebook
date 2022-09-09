import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Col, Row } from 'react-bootstrap';
import moment from 'moment';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser } from '@fortawesome/free-solid-svg-icons';
const mapStateToProps = (state: any) => ({
    isSuperAdmin: state.common.session.roles.includes('SuperAdmin')
});
const mapDispatchToProps = (dispatch: any) => ({});
interface PersonProps {
    name: string;
    surname: string;
    birthday: Date;
    className?: string;
}
const Person = (props: PersonProps): ReactElement => {
    const { t } = useTranslation('person');
    return (
        <div className={`bg-light border rounded-3 m-1 p-2 ${props.className}`}>
            <Row>
                <Col xs={2} className='my-auto text-center'>
                    <FontAwesomeIcon icon={faUser} />
                </Col>
                <Col>
                    <Row>
                        {props.name}
                    </Row>
                    <Row>
                        {props.surname}
                    </Row>
                </Col>
                <Col className='my-auto'>
                    {moment(props.birthday).format('YYYY-MM-DD')}
                </Col>
            </Row>
        </div>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(Person);