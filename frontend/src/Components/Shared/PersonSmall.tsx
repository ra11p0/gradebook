import React, { ReactElement } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Badge, Col, Row } from 'react-bootstrap';
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
    className?: string;
}
const PersonSmall = (props: PersonProps): ReactElement => {
    const { t } = useTranslation('person');
    return (
        <Badge className={`m-1 p-1 ${props.className}`}>
            {`${props.name} `}
            <small>
                {props.surname}
            </small>
        </Badge>
    );
}
export default connect(mapStateToProps, mapDispatchToProps)(PersonSmall);