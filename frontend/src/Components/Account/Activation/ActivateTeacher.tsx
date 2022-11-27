import React, { ReactElement } from 'react';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import { Button, Row } from 'react-bootstrap';

interface ActivateTeacherFormProps {
  defaultOnBackHandler: () => void;
  onSubmit?: () => void;
}

const ActivateTeacherForm = (props: ActivateTeacherFormProps): ReactElement => {
  const { t } = useTranslation();

  const validate = (): any => {
    const errors: any = {};
    return errors;
  };

  const formik = useFormik({
    initialValues: {},
    validate,
    onSubmit: () => {
      if (props.onSubmit) props.onSubmit();
    },
  });

  return (
    <div className="card m-3 p-3">
      <Button onClick={props.defaultOnBackHandler} variant={'link'}>
        {t('back')}
      </Button>
      <Row className="text-center">
        <div>Register teacher</div>
      </Row>
      <form onSubmit={formik.handleSubmit}>
        <Button type="submit">{t('submit')}</Button>
      </form>
    </div>
  );
};

export default ActivateTeacherForm;
