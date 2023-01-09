import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { LoadingButton } from '@mui/lab';
import { List, ListItem } from '@mui/material';
import { ReactElement, useEffect, useState } from 'react';
import { Col, Modal, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import PersonResponse, {
  SimplePersonResponse,
} from '../../../ApiClient/People/Definitions/Responses/PersonResponse';
import PeopleProxy from '../../../ApiClient/People/PeopleProxy';
import getCurrentSchoolRedux from '../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import { GlobalState } from '../../../store';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';
import Person from '../Person';
import IndividualPicker from './IndividualPicker';

interface Props {
  showFilters?: boolean;
  onHide: () => void;
  onConfirm: (peopleGuids: string[]) => void | Promise<void>;
  getPeople: (
    pickerData: PeoplePickerData,
    page: number
  ) => Promise<SimplePersonResponse[]>;
  show: boolean;
  currentSchoolGuid?: string;
  discriminator?: string;
  selectedPeople?: string[];
}

function PeoplePicker(props: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  const [selectedPeople, setSelectedPeople] = useState<string[]>([]);
  const [confirmButtonLoading, setConfirmButtonLoading] = useState(false);

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
          <Col lg="4" className="m-2 p-2 selected-people">
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
                    <div className="d-flex justify-content-between w-100">
                      <FontAwesomeIcon
                        icon={faTimes}
                        className="m-auto pe-3 cursor-pointer text-danger"
                        onClick={() => {
                          setSelectedPeople((s) =>
                            s.filter((p) => p !== item.guid)
                          );
                        }}
                      />
                      <Person {...item} />
                    </div>
                  </ListItem>
                )}
              />
            </List>
          </Col>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <LoadingButton
          type="submit"
          variant="outlined"
          loading={confirmButtonLoading}
          onClick={async () => {
            setConfirmButtonLoading(true);
            await props.onConfirm([...new Set(selectedPeople)]);
            setConfirmButtonLoading(false);
            props.onHide();
          }}
        >
          {t('confirm')}
        </LoadingButton>
      </Modal.Footer>
    </Modal>
  );
}

export default connect(
  (state: GlobalState) => ({
    currentSchoolGuid: getCurrentSchoolRedux(state)?.schoolGuid,
  }),
  () => ({})
)(PeoplePicker);
