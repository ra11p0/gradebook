import { useFormik } from 'formik';
import moment from 'moment';
import { ReactElement, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import ReactDatePicker from 'react-datepicker';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import NewStudentRequest from '../../../../ApiClient/Schools/Definitions/Requests/NewStudentRequest';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../Notifications/Notifications';
import getApplicationLanguageReduxProxy from '../../../../Redux/ReduxQueries/account/getApplicationLanguageRedux';
import getCurrentSchoolReduxProxy from '../../../../Redux/ReduxQueries/account/getCurrentSchoolRedux';
import { GlobalState } from '../../../../store';
import FormikInput from '../../../Shared/FormikInput';

interface formValues {
  name: string;
  surname: string;
  birthday: Date;
}
interface AddNewStudentModalProps {
  show: boolean;
  onHide: () => void;
  currentSchool: any;
  locale: string;
}
const AddNewStudentModal = (props: AddNewStudentModalProps): ReactElement => {
  const { t } = useTranslation('addNewStudentModal');
  const validate = (values: formValues): any => {
    const errors: any = {};
    if (values.name.length < 3) errors.name = t('nameInvalid');
    if (values.surname.length < 3) errors.surname = t('surnameInvalid');
    return errors;
  };
  useEffect(() => {
    formik.resetForm();
  }, [props.show]);
  const formik = useFormik({
    initialValues: {
      name: '',
      surname: '',
      birthday: new Date(),
    },
    validate,
    onSubmit: (values: formValues) => {
      const student: NewStudentRequest = {
        Name: values.name,
        Surname: values.surname,
        Birthday: new Date(values.birthday),
      };
      SchoolsProxy.addNewStudent(
        { ...student, Birthday: moment(student.Birthday).utc().toDate() },
        props.currentSchool.schoolGuid!
      )
        .then(props.onHide)
        .then(() => Notifications.showSuccessNotification())
        .catch(Notifications.showApiError);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t('addStudent')}</Modal.Title>
      </Modal.Header>
      <form onSubmit={formik.handleSubmit}>
        <Modal.Body>
          <div className="m-1 p-1">
            <FormikInput name="name" label={t('name')} formik={formik} />
          </div>
          <div className="m-1 p-1">
            <FormikInput name="surname" label={t('surname')} formik={formik} />
          </div>
          <div className="m-1 p-1">
            <label htmlFor="birthday">{t('birthday')}</label>
            <ReactDatePicker
              selected={formik.values.birthday}
              className="form-control birthday"
              onChange={(evt) => {
                formik.handleChange({
                  target: { name: 'birthday', id: 'birthday', value: evt },
                });
              }}
              locale={props.locale}
              dateFormat="P"
            />
            {formik.errors.birthday && formik.touched.birthday ? (
              <div className="invalid-feedback d-block">
                {formik.errors.birthday as string}
              </div>
            ) : null}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit">{t('addStudent')}</Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};
export default connect(
  (state: GlobalState) => ({
    currentSchool: getCurrentSchoolReduxProxy(state),
    locale: getApplicationLanguageReduxProxy(state),
  }),
  () => ({})
)(AddNewStudentModal);
