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
      registerStudent: require('./locales/en/Account/registerStudent.json'),
      header: require('./locales/en/Shared/header.json'),
      dashboardNavigation: require('./locales/en/Dashboard/dashboardNavigation.json'),
      addNewStudentModal: require('./locales/en/Dashboard/Manage/ManageStudents/addNewStudentModal.json'),
      studentsList: require('./locales/en/Dashboard/Manage/ManageStudents/studentsList.json'),
      profile: require('./locales/en/Account/profile.json'),
      invitations: require('./locales/en/Dashboard/Manage/Invitations/invitations.json'),
      addInvitationModal: require('./locales/en/Dashboard/Manage/Invitations/addInvitationModal.json'),
      registerAdministrator: require('./locales/en/Account/registerAdministrator.json'),
      registerAdministratorPerson: require('./locales/en/Account/registerAdministratorPerson.json'),
      registerAdministratorSchool: require('./locales/en/Account/registerAdministratorSchool.json'),
      schoolSelect: require('./locales/en/Shared/schoolSelect.json'),
      schoolsList: require('./locales/en/Dashboard/Manage/ManageSchool/schoolsList.json'),
      classes: require('./locales/en/Dashboard/Manage/Classes/classes.json'),
      addNewClassModal: require('./locales/en/Dashboard/Manage/Classes/addNewClassModal'),
      settings: require('./locales/en/Dashboard/Manage/Settings/settings'),
    },
    pl: {
      common: require('./locales/pl/common.json'),
      loginForm: require('./locales/pl/Account/loginForm.json'),
      notifications: require('./locales/pl/notifications.json'),
      registerForm: require('./locales/pl/Account/registerForm.json'),
      activateAccount: require('./locales/pl/Account/activateAccount.json'),
      registerStudent: require('./locales/pl/Account/registerStudent.json'),
      header: require('./locales/pl/Shared/header.json'),
      dashboardNavigation: require('./locales/pl/Dashboard/dashboardNavigation.json'),
      addNewStudentModal: require('./locales/pl/Dashboard/Manage/ManageStudents/addNewStudentModal.json'),
      studentsList: require('./locales/pl/Dashboard/Manage/ManageStudents/studentsList.json'),
      profile: require('./locales/pl/Account/profile.json'),
      invitations: require('./locales/pl/Dashboard/Manage/Invitations/invitations.json'),
      addInvitationModal: require('./locales/pl/Dashboard/Manage/Invitations/addInvitationModal.json'),
      registerAdministrator: require('./locales/pl/Account/registerAdministrator.json'),
      registerAdministratorPerson: require('./locales/pl/Account/registerAdministratorPerson.json'),
      registerAdministratorSchool: require('./locales/pl/Account/registerAdministratorSchool.json'),
      schoolSelect: require('./locales/pl/Shared/schoolSelect.json'),
      schoolsList: require('./locales/pl/Dashboard/Manage/ManageSchool/schoolsList.json'),
      classes: require('./locales/pl/Dashboard/Manage/Classes/classes.json'),
      addNewClassModal: require('./locales/pl/Dashboard/Manage/Classes/addNewClassModal'),
      settings: require('./locales/pl/Dashboard/Manage/Settings/settings'),
    }
  },
  ns: ['common', 'loginForm', 'registerForm', 'activateAccount', 'registerStudent', 'header', 'notifications',
    'dashboardNavigation', 'addNewStudentModal', 'studentsList', 'profile', 'invitations',
    'addInvitationModal', 'registerAdministrator', 'registerAdministratorPerson', 'registerAdministratorSchool', 'schoolSelect',
    'schoolsList', "classes", "addNewClassModal", "settings"],
  defaultNS: 'common'
});

i18n.languages = ['en', 'pl'];

export default i18n;
