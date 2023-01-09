import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

const namespaces = [
  'classPicker',
  'common',
  'loginForm',
  'registerForm',
  'activateAccount',
  'header',
  'notifications',
  'manageTeachers',
  'dashboardNavigation',
  'addNewStudentModal',
  'studentsList',
  'profile',
  'invitations',
  'subjects',
  'peoplePicker',
  'educationCycle',
  'addInvitationModal',
  'ActivateAdministrator',
  'ActivateAdministratorPerson',
  'ActivateAdministratorSchool',
  'schoolSelect',
  'schoolsList',
  'classes',
  'addNewClassModal',
  'settings',
  'permissions',
  'personNavigation',
  'classIndex',
  'addNewSubjectModal',
  'addNewTeacherModal',
  'person',
  'service',
  'configureEducationCycleForm',
  'educationCycles',
  'educationCyclesPicker',
  'changePassword',
];

const supportedLngs = ['en', 'pl'];

await i18n.use(initReactI18next).init({
  fallbackNS: 'common',
  defaultNS: 'common',
  fallbackLng: 'en',
  partialBundledLanguages: true,
  supportedLngs,
  ns: namespaces,
  resources: await loadAllResources(namespaces),
});

async function loadAllResources(nss: string[]): Promise<any> {
  if (import.meta.env.MODE === 'test') return {};
  const promises: Array<Promise<any>> = [];
  const ns: any = {};
  for (const namespace of nss) {
    for (const language of supportedLngs) {
      if (!ns[language]) ns[language] = {};
      const promise = import(`./${language}/${namespace}.json`).then((resp) => {
        ns[language][namespace] = resp;
      });
      promises.push(promise);
    }
  }
  await Promise.all(promises);
  return ns;
}

export default i18n;
