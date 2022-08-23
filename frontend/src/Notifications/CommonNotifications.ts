import { Store as NorificationsStore } from 'react-notifications-component';
import i18n from '../i18n/config';

const showCommonError = () => {
    const t = i18n.t;
    NorificationsStore.addNotification({
        title: t('error').toString(),
        message: t('commonErrorMessage').toString(),
        type: 'danger',
        container: 'top-right',
        animationIn: ["animate__animated", "animate__slideInRight"],
        animationOut: ["animate__animated", "animate__fadeOut"],
        dismiss: {
          duration: 5000,
          onScreen: true
        }
    });
};

export default {
    showCommonError
}
