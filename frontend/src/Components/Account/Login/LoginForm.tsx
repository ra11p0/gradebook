import React, { ReactElement, useState } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import AccountProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { useTranslation } from 'react-i18next';
import { Form } from 'react-bootstrap';
import setLoginReduxWrapper from '../../../Redux/ReduxCommands/account/setLoginRedux';
import setAppLoadReduxWrapper from '../../../Redux/ReduxCommands/account/setAppLoadRedux';
import { store } from '../../../store';
import { useFormik } from 'formik';
import FormikInput from '../../Shared/FormikInput';
import * as yup from 'yup';
import LoadingButton from '@mui/lab/LoadingButton';
import Swal from 'sweetalert2';
import { Link } from '@mui/material';
import ChangePasswordModal from '../ChangePassword/ChangePasswordModal';

function LoginForm(): ReactElement {
  const { t } = useTranslation('loginForm');
  const [isLoggingIn, setIsLoggingIn] = useState(false);
  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
    },
    validationSchema: yup.object().shape({
      email: yup
        .string()
        .required(t('emailFieldRequired'))
        .email(t('wrongEmailAddress')),
      password: yup.string().required(t('passwordFieldRequired')),
    }),
    onSubmit: async (values) => {
      setIsLoggingIn(true);
      await AccountProxy.logIn({
        email: values.email,
        password: values.password,
      })
        .then(async (loginResponse) => {
          setAppLoadReduxWrapper(store.dispatch, false);
          await setLoginReduxWrapper({
            refreshToken: loginResponse.data.refresh_token,
            accessToken: loginResponse.data.access_token,
          });
        })
        .catch(async (resp) => {
          await Swal.fire({
            icon: resp.response.status === 302 ? 'warning' : 'error',
            title: t('loginFailed'),
            text:
              resp.response.status === 302
                ? t('activateYourAccountToLogIn')
                : t('couldNotLoginWithTypedCredentials'),
          });
          await formik.setFieldValue('password', '');
        });
      setAppLoadReduxWrapper(store.dispatch, true);
      setIsLoggingIn(false);
    },
  });
  return (
    <>
      <div className="card m-3 p-3">
        <div className="card-body">
          <div className="m-1 p-1 text-center text-secondary">
            <b>{t('loging')}</b>
          </div>
          <form onSubmit={formik.handleSubmit}>
            <Form.Group>
              <FormikInput name="email" label={t('email')} formik={formik} />
              <FormikInput
                testId="password"
                name="password"
                type="password"
                label={t('password')}
                formik={formik}
              />
              <div className="text-end fs-7">
                <ForgotPasswordSection />
              </div>
              <div className="text-center">
                <LoadingButton
                  size="small"
                  loading={isLoggingIn}
                  variant="outlined"
                  type="submit"
                  disabled={isLoggingIn}
                >
                  {t('logIn')}
                </LoadingButton>
              </div>
            </Form.Group>
          </form>
        </div>
      </div>
      <p className="text-center ">
        {t('dontHaveAccount')} &nbsp;
        <RouterLink className="text-secondary" to={'register'}>
          {t('register')}
        </RouterLink>
      </p>
    </>
  );
}

function ForgotPasswordSection(): ReactElement {
  const { t } = useTranslation('loginForm');
  const [changePasswordModalVisible, setChangePasswordModalVisible] =
    useState(false);
  return (
    <>
      <ChangePasswordModal
        show={changePasswordModalVisible}
        onHide={() => setChangePasswordModalVisible(false)}
      />
      <Link
        className="text-secondary cursor-pointer"
        test-id="forgotPassword"
        onClick={() => setChangePasswordModalVisible(true)}
      >
        {t('forgotPassword')}
      </Link>
    </>
  );
}

export default LoginForm;
