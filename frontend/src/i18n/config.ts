import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

const namespaces = [
  'classPicker',
  'common',
  'loginForm',
  'registerForm',
  'activateAccount',
  'ActivateStudent',
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

  /* resources: {
    en: {
      common: await import('./en/common.json'),
      loginForm: await import('./en/loginForm.json'),
      notifications: await import('./en/notifications.json'),
      registerForm: await import('./en/registerForm.json'),
      activateAccount: await import('./en/activateAccount.json'),
      ActivateStudent: await import('./en/ActivateStudent.json'),
      header: await import('./en/header.json'),
      dashboardNavigation: await import('./en/dashboardNavigation.json'),
      addNewStudentModal: await import('./en/addNewStudentModal.json'),
      studentsList: await import('./en/studentsList.json'),
      profile: await import('./en/profile.json'),
      invitations: await import('./en/invitations.json'),
      addInvitationModal: await import('./en/addInvitationModal.json'),
      ActivateAdministrator: await import('./en/ActivateAdministrator.json'),
      ActivateAdministratorPerson: await import(
        './en/ActivateAdministratorPerson.json'
      ),
      ActivateAdministratorSchool: await import(
        './en/ActivateAdministratorSchool.json'
      ),
      schoolSelect: await import('./en/schoolSelect.json'),
      schoolsList: await import('./en/schoolsList.json'),
      classes: await import('./en/classes.json'),
      addNewClassModal: await import('./en/addNewClassModal.json'),
      settings: await import('./en/settings.json'),
      permissions: await import('./en/permissions.json'),
      personNavigation: await import('./en/personNavigation.json'),
      classIndex: await import('./en/classIndex.json'),
      addNewSubjectModal: await import('./en/addNewSubjectModal.json'),
      subjects: await import('./en/subjects.json'),
      peoplePicker: await import('./en/peoplePicker.json'),
      manageTeachers: await import('./en/manageTeachers.json'),
      addNewTeacherModal: await import('./en/addNewTeacherModal.json'),
      person: await import('./en/person.json'),
      educationCycle: await import('./en/educationCycle.json'),
      service: await import('./en/service.json'),
      configureEducationCycleForm: await import(
        './en/configureEducationCycleForm.json'
      ),
      educationCycles: await import('./en/educationCycles.json'),
      educationCyclesPicker: await import('./en/educationCyclesPicker.json'),
      classPicker: await import('./en/classPicker.json'),
      changePassword: await import('./en/changePassword.json'),
    },
    pl: {
      common: await import('./pl/common.json'),
      loginForm: await import('./pl/loginForm.json'),
      notifications: await import('./pl/notifications.json'),
      registerForm: await import('./pl/registerForm.json'),
      activateAccount: await import('./pl/activateAccount.json'),
      ActivateStudent: await import('./pl/ActivateStudent.json'),
      header: await import('./pl/header.json'),
      dashboardNavigation: await import('./pl/dashboardNavigation.json'),
      addNewStudentModal: await import('./pl/addNewStudentModal.json'),
      studentsList: await import('./pl/studentsList.json'),
      profile: await import('./pl/profile.json'),
      invitations: await import('./pl/invitations.json'),
      addInvitationModal: await import('./pl/addInvitationModal.json'),
      ActivateAdministrator: await import('./pl/ActivateAdministrator.json'),
      ActivateAdministratorPerson: await import(
        './pl/ActivateAdministratorPerson.json'
      ),
      ActivateAdministratorSchool: await import(
        './pl/ActivateAdministratorSchool.json'
      ),
      schoolSelect: await import('./pl/schoolSelect.json'),
      schoolsList: await import('./pl/schoolsList.json'),
      classes: await import('./pl/classes.json'),
      addNewClassModal: await import('./pl/addNewClassModal.json'),
      settings: await import('./pl/settings.json'),
      permissions: await import('./pl/permissions.json'),
      personNavigation: await import('./pl/personNavigation.json'),
      classIndex: await import('./pl/classIndex.json'),
      addNewSubjectModal: await import('./pl/addNewSubjectModal.json'),
      subjects: await import('./pl/subjects.json'),
      peoplePicker: await import('./pl/peoplePicker.json'),
      manageTeachers: await import('./pl/manageTeachers.json'),
      addNewTeacherModal: await import('./pl/addNewTeacherModal.json'),
      person: await import('./pl/person.json'),
      educationCycle: await import('./pl/educationCycle.json'),
      service: await import('./pl/service.json'),
      configureEducationCycleForm: await import(
        './pl/configureEducationCycleForm.json'
      ),
      educationCycles: await import('./pl/educationCycles.json'),
      educationCyclesPicker: await import('./pl/educationCyclesPicker.json'),
      classPicker: await import('./pl/classPicker.json'),
      changePassword: await import('./pl/changePassword.json'),
    }, */
  resources: await (async (): Promise<any> => {
    if (import.meta.env.MODE === 'test') return {};
    const ns: any = {};
    for (const namespace of namespaces) {
      for (const language of supportedLngs) {
        if (!ns[language]) ns[language] = {};
        ns[language][namespace] = await import(
          `./${language}/${namespace}.json`
        );
      }
    }
    return ns;
  })(),
});
export default i18n;
