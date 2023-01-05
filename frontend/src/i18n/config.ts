import i18n from 'i18next';
import Backend from 'i18next-http-backend';
import { initReactI18next } from 'react-i18next';

await i18n
  .use(Backend)
  .use(initReactI18next)
  .init({
    load: 'languageOnly',
    fallbackNS: 'common',
    defaultNS: 'common',
    fallbackLng: 'en',
    backend: {
      loadPath: '/locales/{{lng}}/{{ns}}.json',
    },
    react: {
      useSuspense: false,
    },
  });

i18n.languages = ['en', 'pl'];

export default i18n;
