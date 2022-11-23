import i18n from 'i18next';
import setApplicationLanguageReduxWrapper from '../Redux/ReduxCommands/account/setApplicationLanguageRedux';
import { store } from '../store';

i18n.init({
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
      personNavigation: require('./locales/en/Person/personNavigation.json'),
      classIndex: require('./locales/en/Class/classIndex.json'),
      addNewSubjectModal: require('./locales/en/Subjects/addNewSubjectModal.json'),
      subjects: require('./locales/en/Subjects/subjects.json'),
      peoplePicker: require('./locales/en/Shared/peoplePicker.json'),
      manageTeachers: require('./locales/en/Dashboard/Manage/ManageTeachers/manageTeachers.json'),
      addNewTeacherModal: require('./locales/en/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json'),
      person: require('./locales/en/Person/person.json'),
      educationCycle: require('./locales/en/Dashboard/Manage/educationCycle.json')
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
      personNavigation: require('./locales/pl/Person/personNavigation.json'),
      classIndex: require('./locales/pl/Class/classIndex.json'),
      addNewSubjectModal: require('./locales/pl/Subjects/addNewSubjectModal.json'),
      subjects: require('./locales/pl/Subjects/subjects.json'),
      peoplePicker: require('./locales/pl/Shared/peoplePicker.json'),
      manageTeachers: require('./locales/pl/Dashboard/Manage/ManageTeachers/manageTeachers.json'),
      addNewTeacherModal: require('./locales/pl/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json'),
      person: require('./locales/pl/Person/person.json'),
      educationCycle: require('./locales/pl/Dashboard/Manage/educationCycle.json')
    }
  },
  ns: ['common', 'loginForm', 'registerForm', 'activateAccount', 'ActivateStudent', 'header', 'notifications', 'manageTeachers',
    'dashboardNavigation', 'addNewStudentModal', 'studentsList', 'profile', 'invitations', 'subjects', 'peoplePicker', 'educationCycle',
    'addInvitationModal', 'ActivateAdministrator', 'ActivateAdministratorPerson', 'ActivateAdministratorSchool', 'schoolSelect',
    'schoolsList', "classes", "addNewClassModal", "settings", "permissions", 'personNavigation', 'classIndex', 'addNewSubjectModal',
    'addNewTeacherModal', 'person'],
  defaultNS: 'common'
});

i18n.languages = ['en', 'pl'];

i18n.on('languageChanged', (language) => {
  setApplicationLanguageReduxWrapper(store.dispatch, language);
})

export default i18n;
