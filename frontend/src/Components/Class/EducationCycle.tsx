import { faWarning } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { ReactElement, useEffect, useState } from 'react';
import { Alert, Button, Card, Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCyclesInClassResponse from '../../ApiClient/Classes/Definitions/Responses/EducationCyclesInClassResponse';
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import Notifications from '../../Notifications/Notifications';
import EducationCyclePicker from '../Shared/EducationCyclePicker/EducationCyclePicker';
import LoadingScreen from '../Shared/LoadingScreen';

interface Props {
  classGuid: string;
}

function EducationCycle({ classGuid }: Props): ReactElement {
  const { t } = useTranslation('educationCycle');
  const [refreshKey, setRefreshKey] = useState(0);
  const [educationCycle, setEducationCycle] = useState<
    EducationCyclesInClassResponse | undefined
  >(undefined);
  useEffect(() => {
    void (async () => {
      await ClassesProxy.educationCycles
        .getEducationCyclesInClass(classGuid)
        .then((resp) => {
          setEducationCycle(resp.data);
        })
        .catch(Notifications.showApiError);
    })();
  }, [refreshKey]);

  return (
    <Card>
      <Card.Header>
        <Card.Title>{t('educationCycle')}</Card.Title>
      </Card.Header>
      <Card.Body>
        <LoadingScreen isReady={!!educationCycle}>
          <>
            {educationCycle?.activeEducationCycle ? (
              <>
                {educationCycle.activeEducationCycle.name}
                {educationCycle?.activeEducationCycleInstance ? (
                  <>education cycle configured</>
                ) : (
                  <>
                    <Alert variant="warning">
                      <div className="d-flex justify-content-between">
                        <Row>
                          <Col>
                            <div>{t('educationCycleNotConfigured')}</div>
                          </Col>
                          <Col></Col>
                        </Row>
                        <div>
                          <Row>
                            <Col className="text-end">
                              <FontAwesomeIcon icon={faWarning} />
                            </Col>
                          </Row>
                          <Row>
                            <Col>
                              <Button>{t('configureEducationCycle')}</Button>
                            </Col>
                          </Row>
                        </div>
                      </div>
                    </Alert>
                  </>
                )}
              </>
            ) : (
              <>
                <Alert variant="warning">
                  <div className="d-flex justify-content-between">
                    <Row>
                      <Col>
                        <div>{t('educationCycleNotAttached')}</div>
                      </Col>
                      <Col></Col>
                    </Row>
                    <div>
                      <Row>
                        <Col className="text-end">
                          <FontAwesomeIcon icon={faWarning} />
                        </Col>
                      </Row>
                      <Row>
                        <Col>
                          <EducationCyclePicker
                            onCyclesSelected={async (selected) => {
                              const selectedOne = selected.find(() => true);
                              if (!selectedOne) return;
                              await EducationCyclesProxy.setEducationCycleForClass(
                                selectedOne,
                                classGuid
                              );
                              setRefreshKey((e) => e + 1);
                            }}
                            onlyOne={true}
                          />
                        </Col>
                      </Row>
                    </div>
                  </div>
                </Alert>
              </>
            )}
          </>
        </LoadingScreen>
      </Card.Body>
    </Card>
  );
}

export default EducationCycle;
