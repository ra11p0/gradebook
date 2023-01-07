import { faSchool } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ReactElement } from 'react';
import { Col, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';

interface SchoolProps {
  guid: string;
  name: string;
  city: string;
  addresLine: string;
  className?: string;
}
function School(props: SchoolProps): ReactElement {
  return (
    <Link to={`/school/show/${props.guid}`} className={'text-reset school'}>
      <div
        className={`bg-light border rounded-3 m-1 p-2 ${props.className ?? ''}`}
      >
        <Row>
          <Col xs={2} className="my-auto text-center">
            <FontAwesomeIcon icon={faSchool} />
          </Col>
          <Col>
            <Row>{props.name}</Row>
            <Row>{props.city}</Row>
          </Col>
          <Col className="my-auto">{props.addresLine}</Col>
        </Row>
      </div>
    </Link>
  );
}

export default School;
