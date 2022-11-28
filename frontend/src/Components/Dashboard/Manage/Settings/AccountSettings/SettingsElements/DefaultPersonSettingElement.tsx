import { MenuItem, Select } from '@mui/material';
import React, { ReactElement, useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import AccountProxy from '../../../../../../ApiClient/Accounts/AccountsProxy';
import RelatedPersonResponse from '../../../../../../ApiClient/Accounts/Definitions/Responses/RelatedPersonResponse';
import Notifications from '../../../../../../Notifications/Notifications';
import getCurrentUserIdReduxProxy from '../../../../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';
import Person from '../../../../../Shared/Person';
import { connect } from 'react-redux';
import LoadingScreen from '../../../../../Shared/LoadingScreen';

interface Props {
  currentUserGuid?: string;
  onChange: (value: string) => void;
}

function DefaultPersonSettingElement(props: Props): ReactElement {
  const { t } = useTranslation('settings');
  const [people, setPeople] = useState<RelatedPersonResponse[]>(
    [] as RelatedPersonResponse[]
  );
  const [defaultPersonGuid, setDefaultPersonGuid] = useState<string>('');
  useEffect(() => {
    AccountProxy.getRelatedPeople(props.currentUserGuid!)
      .then((peopleResponse) => {
        setPeople(peopleResponse.data);
      })
      .catch(Notifications.showApiError);
    AccountProxy.settings
      .getDefaultPerson(props.currentUserGuid!)
      .then((personGuidResponse) => {
        setDefaultPersonGuid(personGuidResponse.data);
      })
      .catch(Notifications.showApiError);
  }, [props.currentUserGuid]);
  return (
    <LoadingScreen isReady={!!defaultPersonGuid}>
      <>
        <Row>
          <Col className="my-auto">
            <label className="fs-5">{t('defaultPerson')}</label>
          </Col>
          <Col>
            <Select
              className="setDefaultPersonGuidSelect form-control"
              value={
                people.map((e) => e.guid).includes(defaultPersonGuid)
                  ? defaultPersonGuid
                  : ''
              }
              onChange={(e) => {
                setDefaultPersonGuid(e.target.value);
                props.onChange(e.target.value);
              }}
              renderValue={(selected: string) => {
                const person = people.find((p) => p.guid === selected);
                if (!person) throw new Error('person is undefined');
                return `${person?.name} ${person?.surname}`;
              }}
            >
              {people.map((person) => (
                <MenuItem key={person.guid} value={person.guid} className="row">
                  <Person {...person} noLink={true} />
                </MenuItem>
              ))}
            </Select>
          </Col>
        </Row>
      </>
    </LoadingScreen>
  );
}

export default connect(
  (state) => ({
    currentUserGuid: getCurrentUserIdReduxProxy(state),
  }),
  () => ({})
)(DefaultPersonSettingElement);
