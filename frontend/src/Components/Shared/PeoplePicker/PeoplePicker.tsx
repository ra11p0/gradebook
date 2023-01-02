import { Button, List, ListItem } from '@mui/material';
import React, { ReactElement, useEffect, useState } from 'react';
import { Col, Modal, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import PersonResponse from '../../../ApiClient/People/Definitions/Responses/PersonResponse';
import PeopleProxy from '../../../ApiClient/People/PeopleProxy';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';
import Person from '../Person';
import IndividualPicker from './IndividualPicker';

interface Props {
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

  useEffect(() => {
    setSelectedPeople(props.selectedPeople ?? []);
  }, [props.show]);
  return (
    <Modal show={props.show} onHide={props.onHide} size="lg">
      <Modal.Header closeButton>{t('peoplePicker')}</Modal.Header>
      <Modal.Body>
        <Row>
          <Col>
            <IndividualPicker
              {...props}
              setSelectedPeople={setSelectedPeople}
              selectedPeople={selectedPeople}
            />
          </Col>
          <Col xs="4">
            <h5>{t('selectedPeople')}</h5>
            <List
              className="vh-50 overflow-auto"
              id="scrollContainerSelectedPeople"
            >
              <InfiniteScrollWrapper
                scrollableTarget="scrollContainerSelectedPeople"
                effect={[selectedPeople]}
                fetch={async (page: number) => {
                  return (
                    await PeopleProxy.getPeopleDetails(selectedPeople, page)
                  ).data;
                }}
                mapper={(item: PersonResponse, index: number) => (
                  <ListItem key={index} className="p-1 m-0">
                    <Person {...item} />
                  </ListItem>
                )}
              />
            </List>
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
