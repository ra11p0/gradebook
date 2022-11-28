import React, { ReactElement } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import EducationCycleResponse from '../../ApiClient/EducationCycles/Definitions/Responses/EducationCycleResponse';
import Person from '../Shared/Person';

function EducationCycleHeader(props: EducationCycleResponse): ReactElement {
  const { t } = useTranslation('educationCycles');
  return (
    <Row className="bg-light p-3">
      <Col>
        <h2>{props.name}</h2>
      </Col>
      <Col>
        <small>{t('creator')}</small>
        <Person {...props.creator} />
      </Col>
    </Row>
  );
}

export default EducationCycleHeader;
