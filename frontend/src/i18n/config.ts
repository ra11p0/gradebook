import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

i18n.use(initReactI18next).init({
  fallbackNS: 'common',
  fallbackLng: 'en',
  lng: 'pl',
  resources: {
    en: {
      common: require('./locales/en/common.json'),
      loginForm: require('./locales/en/Account/loginForm.json'),
      notifications: require('./locales/en/notifications.json'),
      registerForm: require('./locales/en/Account/registerForm.json'),
      activateAccount: require('./locales/en/Account/activateAccount.json'),
      ActivateStudent: require('./locales/en/Account/ActivateStudent.json'),
      header: require('./locales/en/Shared/header.json'),
      dashboardNavigation: require('./locales/en/Dashboard/dashboardNavigation.json'),
      addNewStudentModal: require('./locales/en/Dashboard/Manage/ManageStudents/addNewStudentModal.json'),
      studentsList: require('./locales/en/Dashboard/Manage/ManageStudents/studentsList.json'),
      profile: require('./locales/en/Account/profile.json'),
      invitations: require('./locales/en/Dashboard/Manage/Invitations/invitations.json'),
      addInvitationModal: require('./locales/en/Dashboard/Manage/Invitations/addInvitationModal.json'),
      ActivateAdministrator: require('./locales/en/Account/ActivateAdministrator.json'),
      ActivateAdministratorPerson: require('./locales/en/Account/ActivateAdministratorPerson.json'),
      ActivateAdministratorSchool: require('./locales/en/Account/ActivateAdministratorSchool.json'),
      schoolSelect: require('./locales/en/Shared/schoolSelect.json'),
      schoolsList: require('./locales/en/Dashboard/Manage/ManageSchool/schoolsList.json'),
      classes: require('./locales/en/Dashboard/Manage/Classes/classes.json'),
      addNewClassModal: require('./locales/en/Dashboard/Manage/Classes/addNewClassModal'),
      settings: require('./locales/en/Dashboard/Manage/Settings/settings'),
      permissions: require('./locales/en/Person/permissions.json'),
    },
    pl: {
      common: require('./locales/pl/common.json'),
      loginForm: require('./locales/pl/Account/loginForm.json'),
      notifications: require('./locales/pl/notifications.json'),
      registerForm: require('./locales/pl/Account/registerForm.json'),
      activateAccount: require('./locales/pl/Account/activateAccount.json'),
      ActivateStudent: require('./locales/pl/Account/ActivateStudent.json'),
      header: require('./locales/pl/Shared/header.json'),
      dashboardNavigation: require('./locales/pl/Dashboard/dashboardNavigation.json'),
      addNewStudentModal: require('./locales/pl/Dashboard/Manage/ManageStudents/addNewStudentModal.json'),
      studentsList: require('./locales/pl/Dashboard/Manage/ManageStudents/studentsList.json'),
      profile: require('./locales/pl/Account/profile.json'),
      invitations: require('./locales/pl/Dashboard/Manage/Invitations/invitations.json'),
      addInvitationModal: require('./locales/pl/Dashboard/Manage/Invitations/addInvitationModal.json'),
      ActivateAdministrator: require('./locales/pl/Account/ActivateAdministrator.json'),
      ActivateAdministratorPerson: require('./locales/pl/Account/ActivateAdministratorPerson.json'),
      ActivateAdministratorSchool: require('./locales/pl/Account/ActivateAdministratorSchool.json'),
      schoolSelect: require('./locales/pl/Shared/schoolSelect.json'),
      schoolsList: require('./locales/pl/Dashboard/Manage/ManageSchool/schoolsList.json'),
      classes: require('./locales/pl/Dashboard/Manage/Classes/classes.json'),
      addNewClassModal: require('./locales/pl/Dashboard/Manage/Classes/addNewClassModal'),
      settings: require('./locales/pl/Dashboard/Manage/Settings/settings'),
      permissions: require('./locales/pl/Person/permissions.json'),
    }
  },
  ns: ['common', 'loginForm', 'registerForm', 'activateAccount', 'ActivateStudent', 'header', 'notifications',
    'dashboardNavigation', 'addNewStudentModal', 'studentsList', 'profile', 'invitations',
    'addInvitationModal', 'ActivateAdministrator', 'ActivateAdministratorPerson', 'ActivateAdministratorSchool', 'schoolSelect',
    'schoolsList', "classes", "addNewClassModal", "settings", "permissions"],
  defaultNS: 'common'
});

i18n.languages = ['en', 'pl'];

export default i18n;
