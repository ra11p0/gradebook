import { Formik } from 'formik';
import React, { ReactElement, useState } from 'react';
import { useTranslation } from 'react-i18next';
import FormikInput from '../../Shared/FormikInput';
import * as yup from 'yup';
import { LoadingButton } from '@mui/lab';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import Swal from 'sweetalert2';

interface Props {
  onRequestCompleted: () => void;
}

function ChangePasswordLoggedIn(props: Props): ReactElement {
  const { t } = useTranslation('registerForm');
  const [isLoading, setIsLoading] = useState(false);
  return (
    <>
      <Formik
        initialValues={{
          oldPassword: '',
          newPassword: '',
          newPasswordConfirm: '',
        }}
        validationSchema={yup.object().shape({
          oldPassword: yup.string().required(t('fieldRequired')),
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
        onSubmit={async (values, ctx) => {
          setIsLoading(true);
          await AccountsProxy.setNewPasswordAuthorized(
            values.newPassword,
            values.newPasswordConfirm,
            values.oldPassword
          )
            .then(async (resp) => {
              await Swal.fire({
                icon: 'success',
                title: t('passwordChanged'),
              });
              props.onRequestCompleted();
            })
            .catch(async (resp) => {
              ctx.setFieldError('oldPassword', t('wrongPassword'));
            });
          setIsLoading(false);
        }}
      >
        {(formik) => (
          <form onSubmit={formik.handleSubmit}>
            <FormikInput
              type="password"
              name="oldPassword"
              label={t('oldPassword')}
              formik={formik}
            />
            <FormikInput
              type="password"
              name="newPassword"
              label={t('newPassword')}
              formik={formik}
            />
            <FormikInput
              type="password"
              name="newPasswordConfirm"
              label={t('newPasswordConfirm')}
              formik={formik}
            />
            <div className="d-flex justify-content-end">
              <LoadingButton
                variant="outlined"
                type="submit"
                loading={isLoading}
              >
                {t('submit')}
              </LoadingButton>
            </div>
          </form>
        )}
      </Formik>
    </>
  );
}

export default ChangePasswordLoggedIn;
