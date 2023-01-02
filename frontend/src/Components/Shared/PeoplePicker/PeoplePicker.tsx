import { Button } from '@mui/material';
import React, { ReactElement, useEffect, useState } from 'react';
import { Col, ListGroup, Modal, Row, Tab, Tabs } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import PersonResponse from '../../../ApiClient/Schools/Definitions/Responses/PersonResponse';
import LoadingScreen from '../LoadingScreen';
import Person from '../Person';
import IndividualPicker from './IndividualPicker';

interface Props {
  dynamic?: boolean;
  onHide: () => void;
  onConfirm: (peopleGuids: string[]) => void;
  getPeople: (
    schoolGuid: string,
    schoolRole: string,
    query: string,
    page: number
  ) => Promise<PersonResponse[]>;
  show: boolean;
  currentSchoolGuid?: string;
  discriminator?: string;
  selectedPeople?: string[];
}

function PeoplePicker(props: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  const [selectedPeople, setSelectedPeople] = useState<string[]>([]);
  const [selectedPeopleModels, setSelectedPeopleModels] = useState<
    PersonResponse[] | undefined
  >(undefined);

  useEffect(() => {
    setSelectedPeopleModels(undefined);
    void (async () => {})();
  }, [selectedPeople]);

  useEffect(() => {
    setSelectedPeople(props.selectedPeople ?? []);
  }, [props.show]);
  return (
    <Modal show={props.show} onHide={props.onHide} size="lg">
      <Modal.Header closeButton>{t('peoplePicker')}</Modal.Header>
      <Modal.Body>
        <Row>
          <Col>
            <Tabs
              defaultActiveKey="individual"
              id="uncontrolled-tab-example"
              className="mb-3"
            >
              <Tab eventKey="individual" title="individual">
                <IndividualPicker
                  {...props}
                  setSelectedPeople={setSelectedPeople}
                  selectedPeople={selectedPeople}
                />
              </Tab>
              <Tab eventKey="profile" title="Profile"></Tab>
            </Tabs>
          </Col>
          <Col xs="4">
            <h5>{t('selectedPeople')}</h5>
            <LoadingScreen isReady={!!selectedPeopleModels}>
              <ListGroup>
                {selectedPeopleModels?.map((el, key) => (
                  <>
                    <Person {...el} key={key} />
                  </>
                ))}
              </ListGroup>
            </LoadingScreen>
          </Col>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button
          variant="outlined"
          onClick={() => {
            props.onConfirm([...new Set(selectedPeople)]);
            props.onHide();
          }}
        >
          {t('confirm')}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default connect(
  (state: any) => ({
    currentSchoolGuid: state.common.school?.schoolGuid,
  }),
  () => ({})
)(PeoplePicker);
