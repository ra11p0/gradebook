import { faChalkboardTeacher } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import moment from 'moment';
import React, { ReactElement } from 'react';
import { Col, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';

interface Props {
  guid: string;
  name: string;
  description?: string;
  createdDate: Date;
  noLink?: boolean;
  className?: string;
}

function Class(props: Props): ReactElement {
  return (
    <Link
      to={props.noLink ? `javascript:;` : `/class/show/${props.guid}`}
      className={`${props.noLink ? 'disabled text-reset' : ''} ${
        props.className ?? ''
      } w-100 text-start btn btn-sm btn-outline-secondary`}
    >
      <Row>
        <Col xs={2} className="my-auto text-center">
          <FontAwesomeIcon icon={faChalkboardTeacher} />
        </Col>
        <Col>
          <Row>{props.name}</Row>
          <Row>{moment.utc(props.createdDate).local().format('Y')}</Row>
        </Col>
        <Col className="my-auto">{props.description}</Col>
      </Row>
    </Link>
  );
}

export default Class;
