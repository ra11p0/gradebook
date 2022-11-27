import React, { ReactElement, useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import GetSchoolResponse from '../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse';
import SchoolsProxy from '../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../Notifications/Notifications';

function SchoolIndex(): ReactElement {
  const { schoolGuid } = useParams();
  const [school, setSchool] = useState<GetSchoolResponse | null>(null);
  useEffect(() => {
    SchoolsProxy.getSchool(schoolGuid!)
      .then((schoolResponse) => {
        setSchool(schoolResponse.data);
      })
      .catch((err) => {
        Notifications.showApiError(err);
      });
  }, []);
  return (
    <div>
      <div className="bg-light p-3">
        <Row>
          <Col xs={'6'}>
            <Row>
              <Col className="my-auto">
                <h2>{school?.name}</h2>
              </Col>
              <Col>
                <Row>
                  <Col className="my-auto">
                    <Row>
                      <Col>{school?.addressLine1}</Col>
                    </Row>
                    {school?.addressLine2 && (
                      <Row>
                        <Col>{school.addressLine2}</Col>
                      </Row>
                    )}
                  </Col>
                  <Col>
                    <Row>
                      <Col className="my-auto">{school?.postalCode}</Col>
                    </Row>
                    <Row>
                      <Col className="my-auto">{school?.city}</Col>
                    </Row>
                  </Col>
                </Row>
              </Col>
            </Row>
          </Col>
        </Row>
      </div>
      <div className="m-4"></div>
    </div>
  );
}

export default SchoolIndex;
