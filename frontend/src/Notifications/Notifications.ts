import { Store as NorificationsStore } from 'react-notifications-component';
import i18n from '../i18n/config';
const t = i18n.t;

const showCommonError = () => {
    NorificationsStore.addNotification({
        title: t('error', { ns: 'notifications' }).toString(),
        message: t('commonErrorMessage', { ns: 'notifications' }).toString(),
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
const showError = (message: string) => {
    NorificationsStore.addNotification({
        title: t('error', { ns: 'notifications' }).toString(),
        message: t(message, { ns: 'notifications' }).toString(),
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
const showApiError = (err: { response: any, message?: string }) => {
    let message = err.response.data.title ?? err.response.data ?? err.message;
    NorificationsStore.addNotification({
        title: t('error', { ns: 'notifications' }).toString(),
        message: t(message, { ns: 'notifications' }).toString(),
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

const showSuccessNotification = (title: string, message: string) => {
    NorificationsStore.addNotification({
        title: t(title, { ns: 'notifications' }).toString(),
        message: t(message, { ns: 'notifications' }).toString(),
        type: 'success',
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
    showCommonError,
    showError,
    showApiError,
    showSuccessNotification
}
