import {
  iNotification,
  Store as NorificationsStore,
} from 'react-notifications-component';
import i18n from '../i18n/config';
const t = i18n.t;

const defaultConfig: iNotification = {
  animationIn: ['animate__animated', 'animate__fadeIn'],
  animationOut: ['animate__animated', 'animate__fadeOut'],
  container: 'top-right',
};

const showCommonError = (): void => {
  NorificationsStore.addNotification({
    ...defaultConfig,
    title: t('error', { ns: 'notifications' }).toString(),
    message: t('commonErrorMessage', { ns: 'notifications' }).toString(),
    type: 'danger',
    dismiss: {
      duration: 5000,
      onScreen: true,
    },
  });
};
const showError = (message: string): void => {
  NorificationsStore.addNotification({
    ...defaultConfig,
    title: t('error', { ns: 'notifications' }).toString(),
    message: t(message, { ns: 'notifications' }).toString(),
    type: 'danger',
    dismiss: {
      duration: 5000,
      onScreen: true,
    },
  });
};
const showApiError = (err: { response: any; message?: string }): void => {
  const message = err.response.data?.title ?? err.response.data ?? err.message;

  NorificationsStore.addNotification({
    ...defaultConfig,
    title: t('error', { ns: 'notifications' }).toString(),
    message: t(typeof message === 'object' ? message.message : message, {
      ns: 'notifications',
    }).toString(),
    type: 'danger',
    dismiss: {
      duration: 5000,
      onScreen: true,
    },
  });
};

const showSuccessNotification = (title: string, message: string): void => {
  NorificationsStore.addNotification({
    ...defaultConfig,
    title: t(title, { ns: 'notifications' }).toString(),
    message: t(message, { ns: 'notifications' }).toString(),
    type: 'success',
    dismiss: {
      duration: 5000,
      onScreen: true,
    },
  });
};

export default {
  showCommonError,
  showError,
  showApiError,
  showSuccessNotification,
};
