import { Formik } from 'formik';
import React, { ReactElement, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';
import FormikInput from '../Shared/FormikInput';
// import NavigateToUrl from '../Shared/NavigateToUrl';
import * as yup from 'yup';
import { Modal, ModalBody, ModalFooter } from 'react-bootstrap';
import { LoadingButton } from '@mui/lab';
import { Button } from '@mui/material';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import Notifications from '../../Notifications/Notifications';
import Swal from 'sweetalert2';

function ResetPassword(): ReactElement {
  const { accountGuid, authCode } = useParams();
  const [isLoading, setIsLoading] = useState(false);

  const [modalIsVisible, setModalIsVisible] = useState(true);
  const { t } = useTranslation('registerForm');
  const navigate = useNavigate();
  useEffect(() => {
    void (async () => {
      if (!accountGuid || !authCode) navigate('/');
    })();
  }, []);
  return (
    <>
      <Modal
        backdrop="static"
        centered
        show={modalIsVisible}
        onExited={() => {
          navigate('/');
        }}
      >
        <Formik
          initialValues={{ newPassword: '', newPasswordConfirm: '' }}
          validationSchema={yup.object().shape({
            newPassword: yup
              .string()
              .required(t('fieldRequired'))
              .max(20, t('passwordTooLong'))
              .min(8, t('passwordTooShort'))
              .matches(/^(?=.*[0-9])/, t('oneNumber'))
              .matches(/^(?=.*[A-Z])/, t('oneUppercase'))
              .matches(/^(?=.*[a-z])/, t('oneLowercase'))
              .matches(/^(?=.*[!@#\\$%\\^&\\*])/, t('oneSpecialCharacter')),
            newPasswordConfirm: yup
              .string()
              .required(t('fieldRequired'))
              .oneOf([yup.ref('newPassword')], t('passwordsNotTheSame')),
          })}
          onSubmit={async (values) => {
            setIsLoading(true);
            await AccountsProxy.setNewPassword(
              values.newPassword,
              values.newPasswordConfirm,
              accountGuid!,
              authCode!
            )
              .then(async (resp) => {
                await Swal.fire({
                  icon: 'success',
                  title: t('passwordChanged'),
                }).then(() => {});
              })
              .catch(Notifications.showApiError);

            setIsLoading(false);
            setModalIsVisible(false);
          }}
        >
          {(formik) => (
            <form onSubmit={formik.handleSubmit}>
              <ModalBody>
                <FormikInput
                  name="newPassword"
                  type="password"
                  label={t('password')}
                  formik={formik}
                />
                <FormikInput
                  name="newPasswordConfirm"
                  type="password"
                  label={t('confirmPassword')}
                  formik={formik}
                />
              </ModalBody>
              <ModalFooter className="gap-2">
                <Button
                  variant="outlined"
                  color="error"
                  onClick={() => {
                    setModalIsVisible(false);
                  }}
                >
                  {t('exit')}
                </Button>
                <LoadingButton
                  variant="outlined"
                  type="submit"
                  loading={isLoading}
                >
                  {t('setNewPassword')}
                </LoadingButton>
              </ModalFooter>
            </form>
          )}
        </Formik>
      </Modal>
    </>
  );
}

export default ResetPassword;
