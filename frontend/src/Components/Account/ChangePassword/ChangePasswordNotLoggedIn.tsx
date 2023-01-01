import { LoadingButton } from '@mui/lab';
import { Formik } from 'formik';
import React, { ReactElement, useState } from 'react';
import FormikInput from '../../Shared/FormikInput';
import * as yup from 'yup';
import AccountsProxy from '../../../ApiClient/Accounts/AccountsProxy';
import Swal from 'sweetalert2';
import Notifications from '../../../Notifications/Notifications';
import { useTranslation } from 'react-i18next';

interface Props {
  onRequestCompleted: () => void;
}

function ChangePasswordNotLoggedIn(props: Props): ReactElement {
  const [isLoading, setIsLoading] = useState(false);
  const { t } = useTranslation('changePassword');
  return (
    <Formik
      initialValues={{ email: '' }}
      validationSchema={yup.object().shape({
        email: yup
          .string()
          .email(t('emailInvalid'))
          .required(t('fieldRequired')),
      })}
      onSubmit={async (values) => {
        setIsLoading(true);
        await AccountsProxy.forgotPassword(values.email)
          .then(async () => {
            await Swal.fire({
              icon: 'success',
              text: t('remindPasswordEmailSent'),
            });
          })
          .catch(Notifications.showApiError);
        setIsLoading(false);
        props.onRequestCompleted();
      }}
    >
      {(formik) => (
        <>
          <form onSubmit={formik.handleSubmit}>
            <FormikInput
              name="email"
              label={t('email')}
              type="email"
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
        </>
      )}
    </Formik>
  );
}

export default ChangePasswordNotLoggedIn;
