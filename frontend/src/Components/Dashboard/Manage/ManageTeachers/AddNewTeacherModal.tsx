import { Button } from '@mui/material';
import { useFormik } from 'formik';
import moment from 'moment';
import { ReactElement } from 'react';
import { Modal } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import NewTeacherRequest from '../../../../ApiClient/Schools/Definitions/Requests/NewTeacherRequest';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../Notifications/Notifications';
import getApplicationLanguageReduxProxy from '../../../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import FormikInput from '../../../Shared/FormikInput';

interface Props {
  show: boolean;
  onHide: () => void;
  currentSchoolGuid?: string;
  locale: string;
}
interface formValues {
  name: string;
  surname: string;
  birthday: Date;
}
function AddNewTeacherModal(props: Props): ReactElement {
  const { t } = useTranslation('addNewTeacherModal');
  const validate = (values: formValues): any => {
    const errors: any = {};
    if (values.name.length < 3) errors.name = t('nameInvalid');
    if (values.surname.length < 3) errors.surname = t('surnameInvalid');
    return errors;
  };
  const formik = useFormik({
    initialValues: {
      name: '',
      surname: '',
      birthday: new Date(),
    },
    validate,
    onSubmit: async (values: formValues) => {
      const teacher: NewTeacherRequest = {
        Name: values.name,
        Surname: values.surname,
        Birthday: new Date(values.birthday),
      };
      await SchoolsProxy.addNewTeacher(
        { ...teacher, Birthday: moment(teacher.Birthday).utc().toDate() },
        props.currentSchoolGuid!
      )
        .then(props.onHide)
        .then(() => Notifications.showSuccessNotification())
        .catch(Notifications.showApiError);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t('addTeacher')}</Modal.Title>
      </Modal.Header>
      <form onSubmit={formik.handleSubmit}>
        <Modal.Body>
          <FormikInput name="name" label={t('name')} formik={formik} />
          <FormikInput name="surname" label={t('surname')} formik={formik} />
          <FormikInput
            name="birthday"
            label={t('birthday')}
            type="date"
            formik={formik}
          />
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit" variant="outlined">
            {t('addTeacher')}
          </Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
}

export default connect(
  (state: any) => ({
    currentSchoolGuid: state.common.school?.schoolGuid,
    locale: getApplicationLanguageReduxProxy(state),
  }),
  () => ({})
)(AddNewTeacherModal);
