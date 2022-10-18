import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

i18n.use(initReactI18next).init({
    fallbackNS: 'common',
    fallbackLng: 'pl',
    lng: 'en',
    resources: {
        en: {
            common: require('./locales/en/common.json'),
        },
        pl: {
            common: require('./locales/pl/common.json'),
        }
    },
    ns: ['common'],
    defaultNS: 'common'
});

i18n.languages = ['en', 'pl'];

export default i18n;
