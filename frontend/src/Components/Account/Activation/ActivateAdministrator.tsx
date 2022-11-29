import React, { ReactElement, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button, Row } from 'react-bootstrap';
import AdministratorsProxy from '../../../ApiClient/Administrators/AdministratorsProxy';
import Notifications from '../../../Notifications/Notifications';
import getCurrentUserIdReduxProxy from '../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';
import setSchoolsListReduxWrapper, {
  setSchoolsListAction,
} from '../../../Redux/ReduxCommands/account/setSchoolsListRedux';
import ActivateAdministratorPerson, {
  ActivateAdministratorPersonValues,
} from './ActivateAdministratorPerson';
import ActivateAdministratorSchool, {
  ActivateAdministratorSchoolValues,
} from './ActivateAdministratorSchool';
import setLoginReduxWrapper from '../../../Redux/ReduxCommands/account/setLoginRedux';
import moment from 'moment';
import getSessionRedux from '../../../Redux/ReduxQueries/account/getSessionRedux';

interface ActivateAdministratorFormProps {
  defaultOnBackHandler: () => void;
  userId: string;
  setSchoolsList: (action: setSchoolsListAction) => void;
  onSubmit?: () => void;
  person?: ActivateAdministratorPersonValues;
}

const ActivateAdministratorForm = (
  props: ActivateAdministratorFormProps
): ReactElement => {
  const { t } = useTranslation('ActivateAdministrator');
  const [person, setPerson] =
    useState<ActivateAdministratorPersonValues | null>(null);
  const [showNewSchoolComponent, setShowNewSchoolComponent] = useState(false);

  useEffect(() => {
    if (props.person) {
      setPerson(props.person);
      setShowNewSchoolComponent(true);
    }
  }, []);

  function activateWithSchool(
    person: ActivateAdministratorPersonValues,
    school: ActivateAdministratorSchoolValues
  ): void {
    AdministratorsProxy.newAdministratorWithSchool(
      { ...person, birthday: moment(person.birthday).utc().toDate() },
      school
    )
      .then(async (response) => {
        const session = getSessionRedux();
        if (!session) return;
        await setLoginReduxWrapper({
          accessToken: session.accessToken,
          refreshToken: session.refreshToken,
        });
        if (props.onSubmit) props.onSubmit();
      })
      .catch(Notifications.showApiError);
  }

  return (
    <div className="card m-3 p-3">
      <Button
        onClick={() => {
          if (showNewSchoolComponent) setShowNewSchoolComponent(false);
          else props.defaultOnBackHandler();
        }}
        variant={'link'}
      >
        {t('back')}
      </Button>
      <Row className="text-center">
        <div className="h4">{t('ActivateAdministrator')}</div>
      </Row>
      <>
        {!(showNewSchoolComponent && person) ? (
          <ActivateAdministratorPerson
            onSubmit={(values: ActivateAdministratorPersonValues) => {
              setPerson(values);
              setShowNewSchoolComponent(true);
            }}
            name={person?.name}
            surname={person?.surname}
            birthday={person?.birthday}
          />
        ) : (
          <>
            <ActivateAdministratorSchool
              onSubmit={(values: ActivateAdministratorSchoolValues) => {
                activateWithSchool(person, values);
              }}
            />
          </>
        )}
      </>
    </div>
  );
};

export default connect(
  (state) => ({
    userId: getCurrentUserIdReduxProxy(state),
  }),
  (dispatch) => ({
    setSchoolsList: (action: setSchoolsListAction) =>
      setSchoolsListReduxWrapper(dispatch, action),
  })
)(ActivateAdministratorForm);
