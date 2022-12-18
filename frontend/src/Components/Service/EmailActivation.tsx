import React, { ReactElement, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';
import Swal from 'sweetalert2';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import NavigateToUrl from '../Shared/NavigateToUrl';

function EmailActivation(): ReactElement {
  const { accountGuid, activationCode } = useParams();
  const { t } = useTranslation('service');
  useEffect(() => {
    void (async () => {
      if (!accountGuid || !activationCode) return;

      await AccountsProxy.verifyEmailAddress(accountGuid, activationCode)
        .then(async () => {
          await Swal.fire({
            icon: 'success',
            title: t('userActivationSuccessAlertTitle'),
            text: t('userActivationSuccessAlertText'),
          });
        })
        .catch(async () => {
          await Swal.fire({
            icon: 'error',
            title: t('userActivationFailedAlertTitle'),
            text: t('userActivationFailedAlertText'),
          });
        });
    })();
  }, []);
  return <NavigateToUrl url={'/'} />;
}

export default EmailActivation;
