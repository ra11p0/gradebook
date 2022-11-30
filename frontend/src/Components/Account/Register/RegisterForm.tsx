import React, { ReactElement, useState } from 'react';
import { Link } from 'react-router-dom';
import { useFormik } from 'formik';
import { useTranslation } from 'react-i18next';
import AccountProxy from '../../../ApiClient/Accounts/AccountsProxy';
import Swal from 'sweetalert2';
import * as yup from 'yup';
import FormikInput from '../../Shared/FormikInput';
import { LoadingButton } from '@mui/lab';
import Notifications from '../../../Notifications/Notifications';

function RegisterForm(): ReactElement {
  const { t } = useTranslation('registerForm');
  const [isRegistering, setIsRegistering] = useState(false);

  const formik = useFormik({
    initialValues: {
      email: '',
      password: '',
      password2: '',
      termsAndConditions: false,
    },
    validationSchema: yup.object().shape({
      email: yup.string().required(t('fieldRequired')).email(t('emailInvalid')),
      termsAndConditions: yup.boolean().isTrue(t('termsConditionsRequired')),
      password: yup
        .string()
        .required(t('fieldRequired'))
        .max(20, t('passwordTooLong'))
        .min(8, t('passwordTooShort'))
        .matches(/^(?=.*[0-9])/, t('oneNumber'))
        .matches(/^(?=.*[A-Z])/, t('oneUppercase'))
        .matches(/^(?=.*[a-z])/, t('oneLowercase'))
        .matches(/^(?=.*[!@#\\$%\\^&\\*])/, t('oneSpecialCharacter')),
      password2: yup
        .string()
        .required(t('fieldRequired'))
        .oneOf([yup.ref('password')], t('passwordsNotTheSame')),
    }),
    onSubmit: async (values) => {
      setIsRegistering(true);
      await AccountProxy.register(values)
        .then(async () => {
          await Swal.fire({
            title: t('userRegisteredAlertTitle'),
            text: t('userRegisteredAlertText'),
          });
        })
        .catch(Notifications.showApiError);
      setIsRegistering(false);
    },
  });

  return (
    <>
      <div className="card m-3 p-3">
        <div className="card-body">
          <form onSubmit={formik.handleSubmit}>
            <div className="m-1 p-1 text-center text-secondary">
              <b>{t('freeSignUp')}</b>
            </div>
            <FormikInput name="email" formik={formik} label={t('email')} />
            <FormikInput
              testId="password"
              name="password"
              formik={formik}
              label={t('password')}
              type="password"
            />
            <FormikInput
              testId="password2"
              name="password2"
              type="password"
              label={t('confirmPassword')}
              formik={formik}
            />
            <FormikInput
              name="termsAndConditions"
              type="switch"
              label={t('termsAndConditions')}
              formik={formik}
            />
            <div className="m-1 p-1 d-flex justify-content-center">
              <LoadingButton
                size="small"
                loading={isRegistering}
                variant="outlined"
                type="submit"
                disabled={isRegistering}
              >
                {t('registerButtonLabel')}
              </LoadingButton>
            </div>
          </form>
        </div>
      </div>
      <p className="text-center ">
        {t('alreadyHaveAccount')} &nbsp;
        <Link className="text-secondary" to={'/'}>
          {t('login')}
        </Link>
      </p>
    </>
  );
}

export default RegisterForm;
