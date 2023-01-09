import { LoadingButton } from '@mui/lab';
import { useFormik } from 'formik';
import moment from 'moment';
import { ReactElement, useState } from 'react';
import { Button, Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import InvitationsProxy from '../../../ApiClient/Invitations/InvitationsProxy';
import PeopleProxy from '../../../ApiClient/People/PeopleProxy';
import SchoolRolesEnum from '../../../Common/Enums/SchoolRolesEnum';
import Notifications from '../../../Notifications/Notifications';
import setLoginReduxWrapper from '../../../Redux/ReduxCommands/account/setLoginRedux';
import getCurrentUserIdReduxProxy from '../../../Redux/ReduxQueries/account/getCurrentUserIdRedux';
import getSessionRedux from '../../../Redux/ReduxQueries/account/getSessionRedux';

interface ActivateWithCodeFormProps {
  defaultOnBackHandler: () => void;
  userId?: string;
  onSubmit?: () => void;
}

interface ActivateWithCodeFormValues {
  accessCode: string;
}

const ActivateWithCodeForm = (
  props: ActivateWithCodeFormProps
): ReactElement => {
  const { t } = useTranslation('activateAccount');
  const [isLoading, setIsLoading] = useState(false);
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [birthday, setBirthday] = useState('');
  const [schoolRole, setSchoolRole] = useState<SchoolRolesEnum | undefined>(
    undefined
  );

  const validate = (values: ActivateWithCodeFormValues): any => {
    const errors: any = {};
    if (values.accessCode.length !== 6) {
      errors.accessCode = t('wrongAccessCodeLength');
    }
    return errors;
  };

  const formik = useFormik({
    initialValues: {
      accessCode: '',
    },
    validate,
    onSubmit: async (values: ActivateWithCodeFormValues) => {
      setIsLoading(true);
      await PeopleProxy.activatePerson(values.accessCode)
        .then(() => {
          const session = getSessionRedux();
          if (!session) return;
          void setLoginReduxWrapper(session);
          if (props.onSubmit) props.onSubmit();
        })
        .catch(Notifications.showApiError);
      setIsLoading(false);
    },
  });

  const handleAccessCodeChange = function (e: any): void {
    if (e.target.value.length === 6) {
      InvitationsProxy.getInvitationDetails(e.target.value)
        .then((resp) => {
          const data = resp.data;
          setName(data.person.name);
          setSurname(data.person.surname);
          setBirthday(moment.utc(data.person.birthday).local().format('L'));
          setSchoolRole(data.person.schoolRole);
        })
        .catch(() => {
          setName('');
          setSurname('');
          setBirthday('');
          setSchoolRole(undefined);
        })
        .catch(Notifications.showApiError);
    }
  };

  return (
    <div className="card m-3 p-3">
      <Button onClick={props.defaultOnBackHandler} variant={'link'}>
        {t('back')}
      </Button>
      <Row className="text-center">
        <div className="h4">{t('ActivateWithCode')}</div>
      </Row>
      <form onSubmit={formik.handleSubmit}>
        <div className="m-1 p-1">
          <label htmlFor="accessCode">{t('accessCode')}</label>
          <input
            className="form-control"
            id="accessCode"
            name="accessCode"
            type="text"
            onChange={formik.handleChange}
            value={formik.values.accessCode}
            onInput={handleAccessCodeChange}
          />
          {formik.errors.accessCode && formik.touched.accessCode ? (
            <div className="invalid-feedback d-block">
              {formik.errors.accessCode}
            </div>
          ) : null}
        </div>
        <Row>
          <Col xs={12} lg={6}>
            <div className="m-1 p-1">
              <label>{t('name')}</label>
              <input
                data-testid="nameField"
                className="form-control"
                type="text"
                defaultValue={name}
                disabled
              />
            </div>
          </Col>
          <Col xs={12} lg={6}>
            <div className="m-1 p-1">
              <label>{t('surname')}</label>
              <input
                data-testid="surnameField"
                className="form-control"
                type="text"
                defaultValue={surname}
                disabled
              />
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <div className="m-1 p-1">
              <label>{t('birthday')}</label>
              <input
                className="form-control"
                type="text"
                defaultValue={birthday}
                disabled
              />
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <div className="m-1 p-1">
              <label>{t('role')}</label>
              <input
                className="form-control"
                type="text"
                defaultValue={schoolRole ? t(SchoolRolesEnum[schoolRole]) : ''}
                disabled
              />
            </div>
          </Col>
        </Row>
        <LoadingButton variant="contained" type="submit" loading={isLoading}>
          {t('confirmInformation')}
        </LoadingButton>
      </form>
    </div>
  );
};

export default connect(
  (state) => ({
    userId: getCurrentUserIdReduxProxy(state),
  }),
  () => ({})
)(ActivateWithCodeForm);
