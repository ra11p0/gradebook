import i18n from 'i18next';
void i18n.init({
  fallbackNS: 'common',
  fallbackLng: 'en',
  lng: 'en',
  resources: {
    en: {
      common: await import('./locales/en/common.json'),
      loginForm: await import('./locales/en/Account/loginForm.json'),
      notifications: await import('./locales/en/notifications.json'),
      registerForm: await import('./locales/en/Account/registerForm.json'),
      activateAccount: await import(
        './locales/en/Account/activateAccount.json'
      ),
      ActivateStudent: await import(
        './locales/en/Account/ActivateStudent.json'
      ),
      header: await import('./locales/en/Shared/header.json'),
      dashboardNavigation: await import(
        './locales/en/Dashboard/dashboardNavigation.json'
      ),
      addNewStudentModal: await import(
        './locales/en/Dashboard/Manage/ManageStudents/addNewStudentModal.json'
      ),
      studentsList: await import(
        './locales/en/Dashboard/Manage/ManageStudents/studentsList.json'
      ),
      profile: await import('./locales/en/Account/profile.json'),
      invitations: await import(
        './locales/en/Dashboard/Manage/Invitations/invitations.json'
      ),
      addInvitationModal: await import(
        './locales/en/Dashboard/Manage/Invitations/addInvitationModal.json'
      ),
      ActivateAdministrator: await import(
        './locales/en/Account/ActivateAdministrator.json'
      ),
      ActivateAdministratorPerson: await import(
        './locales/en/Account/ActivateAdministratorPerson.json'
      ),
      ActivateAdministratorSchool: await import(
        './locales/en/Account/ActivateAdministratorSchool.json'
      ),
      schoolSelect: await import('./locales/en/Shared/schoolSelect.json'),
      schoolsList: await import(
        './locales/en/Dashboard/Manage/ManageSchool/schoolsList.json'
      ),
      classes: await import(
        './locales/en/Dashboard/Manage/Classes/classes.json'
      ),
      addNewClassModal: await import(
        './locales/en/Dashboard/Manage/Classes/addNewClassModal.json'
      ),
      settings: await import(
        './locales/en/Dashboard/Manage/Settings/settings.json'
      ),
      permissions: await import('./locales/en/Person/permissions.json'),
      personNavigation: await import(
        './locales/en/Person/personNavigation.json'
      ),
      classIndex: await import('./locales/en/Class/classIndex.json'),
      addNewSubjectModal: await import(
        './locales/en/Subjects/addNewSubjectModal.json'
      ),
      subjects: await import('./locales/en/Subjects/subjects.json'),
      peoplePicker: await import('./locales/en/Shared/peoplePicker.json'),
      manageTeachers: await import(
        './locales/en/Dashboard/Manage/ManageTeachers/manageTeachers.json'
      ),
      addNewTeacherModal: await import(
        './locales/en/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json'
      ),
      person: await import('./locales/en/Person/person.json'),
      educationCycle: await import(
        './locales/en/Dashboard/Manage/educationCycle.json'
      ),
      service: await import('./locales/en/service.json'),
      configureEducationCycleForm: await import(
        './locales/en/EducationCycle/configureEducationCycleForm.json'
      ),
      educationCycles: await import(
        './locales/en/EducationCycle/educationCycles.json'
      ),
      educationCyclesPicker: await import(
        './locales/en/EducationCycle/educationCyclesPicker.json'
      ),
      classPicker: await import('./locales/en/Shared/classPicker.json'),
    },
    pl: {
      common: await import('./locales/pl/common.json'),
      loginForm: await import('./locales/pl/Account/loginForm.json'),
      notifications: await import('./locales/pl/notifications.json'),
      registerForm: await import('./locales/pl/Account/registerForm.json'),
      activateAccount: await import(
        './locales/pl/Account/activateAccount.json'
      ),
      ActivateStudent: await import(
        './locales/pl/Account/ActivateStudent.json'
      ),
      header: await import('./locales/pl/Shared/header.json'),
      dashboardNavigation: await import(
        './locales/pl/Dashboard/dashboardNavigation.json'
      ),
      addNewStudentModal: await import(
        './locales/pl/Dashboard/Manage/ManageStudents/addNewStudentModal.json'
      ),
      studentsList: await import(
        './locales/pl/Dashboard/Manage/ManageStudents/studentsList.json'
      ),
      profile: await import('./locales/pl/Account/profile.json'),
      invitations: await import(
        './locales/pl/Dashboard/Manage/Invitations/invitations.json'
      ),
      addInvitationModal: await import(
        './locales/pl/Dashboard/Manage/Invitations/addInvitationModal.json'
      ),
      ActivateAdministrator: await import(
        './locales/pl/Account/ActivateAdministrator.json'
      ),
      ActivateAdministratorPerson: await import(
        './locales/pl/Account/ActivateAdministratorPerson.json'
      ),
      ActivateAdministratorSchool: await import(
        './locales/pl/Account/ActivateAdministratorSchool.json'
      ),
      schoolSelect: await import('./locales/pl/Shared/schoolSelect.json'),
      schoolsList: await import(
        './locales/pl/Dashboard/Manage/ManageSchool/schoolsList.json'
      ),
      classes: await import(
        './locales/pl/Dashboard/Manage/Classes/classes.json'
      ),
      addNewClassModal: await import(
        './locales/pl/Dashboard/Manage/Classes/addNewClassModal.json'
      ),
      settings: await import(
        './locales/pl/Dashboard/Manage/Settings/settings.json'
      ),
      permissions: await import('./locales/pl/Person/permissions.json'),
      personNavigation: await import(
        './locales/pl/Person/personNavigation.json'
      ),
      classIndex: await import('./locales/pl/Class/classIndex.json'),
      addNewSubjectModal: await import(
        './locales/pl/Subjects/addNewSubjectModal.json'
      ),
      subjects: await import('./locales/pl/Subjects/subjects.json'),
      peoplePicker: await import('./locales/pl/Shared/peoplePicker.json'),
      manageTeachers: await import(
        './locales/pl/Dashboard/Manage/ManageTeachers/manageTeachers.json'
      ),
      addNewTeacherModal: await import(
        './locales/pl/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json'
      ),
      person: await import('./locales/pl/Person/person.json'),
      educationCycle: await import(
        './locales/pl/Dashboard/Manage/educationCycle.json'
      ),
      service: await import('./locales/pl/service.json'),
      configureEducationCycleForm: await import(
        './locales/pl/EducationCycle/configureEducationCycleForm.json'
      ),
      educationCycles: await import(
        './locales/pl/EducationCycle/educationCycles.json'
      ),
      educationCyclesPicker: await import(
        './locales/pl/EducationCycle/educationCyclesPicker.json'
      ),
      classPicker: await import('./locales/pl/Shared/classPicker.json'),
    },
  },
  ns: [
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
  ],
  defaultNS: 'common',
});

i18n.languages = ['en', 'pl'];

export default i18n;
