import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

i18n.use(initReactI18next).init({
  fallbackLng: 'en',
  lng: 'en',
  resources: {
    en: {
      translations: require('./locales/en/translations.json'),
      loginForm: require('./locales/en/loginForm.json'),
      notifications: require('./locales/en/notifications.json'),
    },
    pl: {
      translations: require('./locales/pl/translations.json'),
      loginForm: require('./locales/pl/loginForm.json'),
      notifications: require('./locales/pl/notifications.json'),
    }
  },
  ns: ['translations', 'loginForm', 'notifications'],
  defaultNS: 'translations'
});

i18n.languages = ['en', 'pl'];

export default i18n;
