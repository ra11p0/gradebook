import React, { ReactElement, useEffect } from 'react';
import { connect } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { Button, Modal } from 'react-bootstrap';
import { useFormik } from 'formik';
import SchoolsProxy from '../../../../ApiClient/Schools/SchoolsProxy';
import Notifications from '../../../../Notifications/Notifications';
import * as Yup from 'yup';
import FormikInput from '../../../Shared/FormikInput';

interface formValues {
  name: string;
}
interface Props {
  show: boolean;
  onHide: () => void;
}
const NewSubjectModal = (props: Props): ReactElement => {
  const { t } = useTranslation('addNewSubjectModal');
  useEffect(() => {
    formik.resetForm();
  }, [props.show]);
  const formik = useFormik({
    initialValues: {
      name: '',
    },
    validationSchema: Yup.object().shape({
      name: Yup.string().required(t('nameIsRequired')),
    }),
    onSubmit: (values: formValues) => {
      SchoolsProxy.subjects
        .addNewSubject(values)
        .then(props.onHide)
        .catch(Notifications.showApiError);
    },
  });
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{t('addSubject')}</Modal.Title>
      </Modal.Header>
      <form onSubmit={formik.handleSubmit}>
        <Modal.Body>
          <div className="m-1 p-1">
            <FormikInput name={'name'} label={t('name')} formik={formik} />
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button type="submit">{t('addSubject')}</Button>
        </Modal.Footer>
      </form>
    </Modal>
  );
};
export default connect(
  () => ({}),
  () => ({})
)(NewSubjectModal);
