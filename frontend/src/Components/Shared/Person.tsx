import { faUser } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ReactElement } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
interface PersonProps {
  guid: string;
  name: string;
  surname: string;
  birthday: Date;
  className?: string;
  noLink?: boolean;
  schoolRole: SchoolRolesEnum;
}
const Person = (props: PersonProps): ReactElement => {
  const { t } = useTranslation('person');
  return (
    <Link
      to={props.noLink ? `javascript:;` : `/person/show/${props.guid}`}
      className={`${props.noLink ? 'disabled text-reset' : ''} ${
        props.className ?? ''
      } w-100 text-start btn btn-sm btn-outline-secondary`}
    >
      <Row data-testid={`personComponent-${props.guid}`}>
        <Col xs={1} className={`my-auto mx-2`}>
          <FontAwesomeIcon icon={faUser} />
        </Col>
        <Col>
          <Row>
            <Col>{`${props.name} ${props.surname}`}</Col>
          </Row>
          <Row>
            <Col>
              <small>{t(SchoolRolesEnum[props.schoolRole])}</small>
            </Col>
          </Row>
        </Col>
      </Row>
    </Link>
  );
};
export default Person;
