import React, { ReactElement, useState } from 'react';
import { Link } from 'react-router-dom';
import AccountProxy from '../../../ApiClient/Accounts/AccountsProxy';
import { useTranslation } from 'react-i18next';
import { Form, Alert } from 'react-bootstrap';
import setLoginReduxWrapper from '../../../Redux/ReduxCommands/account/setLoginRedux';
import setAppLoadReduxWrapper from '../../../Redux/ReduxCommands/account/setAppLoadRedux';
import { store } from '../../../store';
import { useFormik } from 'formik';
import FormikInput from '../../Shared/FormikInput';
import * as yup from 'yup';
import LoadingButton from '@mui/lab/LoadingButton';

function LoginForm(): ReactElement {
  const { t } = useTranslation('loginForm');
  const [loginFailed, setLoginFailed] = useState(false);
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
          setAppLoadReduxWrapper(store.dispatch, true);
        })
        .catch(() => {
          setLoginFailed(true);
          formik.resetForm();
        });
      setIsLoggingIn(false);
    },
  });
  return (
    <div className="card m-3 p-3">
      <div className="card-body">
        <div className="m-1 p-1 display-6">
          <label>{t('loging')}</label>
        </div>
        <form onSubmit={formik.handleSubmit}>
          <Form.Group>
            {loginFailed && (
              <Alert
                variant="danger"
                onClose={() => setLoginFailed(false)}
                dismissible
              >
                {t('loginFailed')}
              </Alert>
            )}
            <FormikInput name="email" label={t('email')} formik={formik} />
            <FormikInput
              name="password"
              type="password"
              label={t('password')}
              formik={formik}
            />
            <div className="m-1 p-1 d-flex flex-wrap justify-content-between">
              <div className="my-auto d-flex gap-2">
                <Link
                  className="btn btn-sm btn-outline-secondary h-100 my-auto"
                  to={'register'}
                >
                  {t('register')}
                </Link>
                <Link className="btn btn-sm btn-outline-secondary" to={''}>
                  {t('changePassword')}
                </Link>
                <Link className="btn btn-sm btn-outline-secondary" to={''}>
                  {t('recoverAccess')}
                </Link>
              </div>
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
  );
}

export default LoginForm;
