import i18n from 'i18next';

import enCommonJson from './locales/en/common.json';
import plCommonJson from './locales/pl/common.json';

import enLoginFormJson from './locales/en/Account/loginForm.json';
import plLoginFormJson from './locales/pl/Account/loginForm.json';

import enNotificationsJson from './locales/en/notifications.json';
import plNotificationsJson from './locales/pl/notifications.json';

import enRegisterFormJson from './locales/en/Account/registerForm.json';
import plRegisterFormJson from './locales/pl/Account/registerForm.json';

import enActivateAccountJson from './locales/en/Account/activateAccount.json';
import plActivateAccountJson from './locales/pl/Account/activateAccount.json';

import enActivateStudentJson from './locales/en/Account/ActivateStudent.json';
import plActivateStudentJson from './locales/pl/Account/ActivateStudent.json';

import enHeaderJson from './locales/en/Shared/header.json';
import plHeaderJson from './locales/pl/Shared/header.json';

import enDashboardNavigationJson from './locales/en/Dashboard/dashboardNavigation.json';
import plDashboardNavigationJson from './locales/pl/Dashboard/dashboardNavigation.json';

import enAddNewStudentModalJson from './locales/en/Dashboard/Manage/ManageStudents/addNewStudentModal.json';
import plAddNewStudentModalJson from './locales/pl/Dashboard/Manage/ManageStudents/addNewStudentModal.json';

import enStudentsListJson from './locales/en/Dashboard/Manage/ManageStudents/studentsList.json';
import plStudentsListJson from './locales/pl/Dashboard/Manage/ManageStudents/studentsList.json';

import enProfileJson from './locales/en/Account/profile.json';
import plProfileJson from './locales/pl/Account/profile.json';

import enInvitationsJson from './locales/en/Dashboard/Manage/Invitations/invitations.json';
import plInvitationsJson from './locales/pl/Dashboard/Manage/Invitations/invitations.json';

import enAddInvitationModalJson from './locales/en/Dashboard/Manage/Invitations/addInvitationModal.json';
import plAddInvitationModalJson from './locales/pl/Dashboard/Manage/Invitations/addInvitationModal.json';

import enActivateAdministrator from './locales/en/Account/ActivateAdministrator.json';
import plActivateAdministrator from './locales/pl/Account/ActivateAdministrator.json';

import enActivateAdministratorPersonJson from './locales/en/Account/ActivateAdministratorPerson.json';
import plActivateAdministratorPersonJson from './locales/pl/Account/ActivateAdministratorPerson.json';

import enActivateAdministratorSchoolJson from './locales/en/Account/ActivateAdministratorSchool.json';
import plActivateAdministratorSchoolJson from './locales/pl/Account/ActivateAdministratorSchool.json';

import enSchoolSelectJson from './locales/en/Shared/schoolSelect.json';
import plSchoolSelectJson from './locales/pl/Shared/schoolSelect.json';

import enSchoolsListJson from './locales/en/Dashboard/Manage/ManageSchool/schoolsList.json';
import plSchoolsListJson from './locales/pl/Dashboard/Manage/ManageSchool/schoolsList.json';

import enClassesJson from './locales/en/Dashboard/Manage/Classes/classes.json';
import plClassesJson from './locales/pl/Dashboard/Manage/Classes/classes.json';

import enAddNewClassModalJson from './locales/en/Dashboard/Manage/Classes/addNewClassModal.json';
import plAddNewClassModalJson from './locales/pl/Dashboard/Manage/Classes/addNewClassModal.json';

import enSettingsJson from './locales/en/Dashboard/Manage/Settings/settings.json';
import plSettingsJson from './locales/pl/Dashboard/Manage/Settings/settings.json';

import enPermissionsJson from './locales/en/Person/permissions.json';
import plPermissionsJson from './locales/pl/Person/permissions.json';

import enPersonNavigationJson from './locales/en/Person/personNavigation.json';
import plPersonNavigationJson from './locales/pl/Person/personNavigation.json';

import enClassIndexJson from './locales/en/Class/classIndex.json';
import plClassIndexJson from './locales/pl/Class/classIndex.json';

import enAddNewSubjectModalJson from './locales/en/Subjects/addNewSubjectModal.json';
import plAddNewSubjectModalJson from './locales/pl/Subjects/addNewSubjectModal.json';

import enSubjectsjson from './locales/en/Subjects/subjects.json';
import plSubjectsjson from './locales/pl/Subjects/subjects.json';

import enPeoplePickerJson from './locales/en/Shared/peoplePicker.json';
import plPeoplePickerJson from './locales/pl/Shared/peoplePicker.json';

import enManageTeachersJson from './locales/en/Dashboard/Manage/ManageTeachers/manageTeachers.json';
import plManageTeachersJson from './locales/pl/Dashboard/Manage/ManageTeachers/manageTeachers.json';

import enAddNewTeacherModalJson from './locales/en/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json';
import plAddNewTeacherModalJson from './locales/pl/Dashboard/Manage/ManageTeachers/addNewTeacherModal.json';

import enPersonJson from './locales/en/Person/person.json';
import plPersonJson from './locales/pl/Person/person.json';

import enEducationCycleJson from './locales/en/Dashboard/Manage/educationCycle.json';
import plEducationCycleJson from './locales/pl/Dashboard/Manage/educationCycle.json';

import enServiceJson from './locales/en/service.json';
import plServiceJson from './locales/pl/service.json';

import enConfigureEducationCycleJson from './locales/en/EducationCycle/configureEducationCycleForm.json';
import plConfigureEducationCycleJson from './locales/pl/EducationCycle/configureEducationCycleForm.json';

import enEducationCyclesJson from './locales/en/EducationCycle/educationCycles.json';
import plEducationCyclesJson from './locales/pl/EducationCycle/educationCycles.json';

import enEducationCyclesPickerJson from './locales/en/EducationCycle/educationCyclesPicker.json';
import plEducationCyclesPickerJson from './locales/pl/EducationCycle/educationCyclesPicker.json';

import enClassPickerJson from './locales/en/Shared/classPicker.json';
import plClassPickerJson from './locales/pl/Shared/classPicker.json';

void i18n.init({
  fallbackNS: 'common',
  fallbackLng: 'en',
  lng: 'en',
  resources: {
    en: {
      common: enCommonJson,
      loginForm: enLoginFormJson,
      notifications: enNotificationsJson,
      registerForm: enRegisterFormJson,
      activateAccount: enActivateAccountJson,
      ActivateStudent: enActivateStudentJson,
      header: enHeaderJson,
      dashboardNavigation: enDashboardNavigationJson,
      addNewStudentModal: enAddNewStudentModalJson,
      studentsList: enStudentsListJson,
      profile: enProfileJson,
      invitations: enInvitationsJson,
      addInvitationModal: enAddInvitationModalJson,
      ActivateAdministrator: enActivateAdministrator,
      ActivateAdministratorPerson: enActivateAdministratorPersonJson,
      ActivateAdministratorSchool: enActivateAdministratorSchoolJson,
      schoolSelect: enSchoolSelectJson,
      schoolsList: enSchoolsListJson,
      classes: enClassesJson,
      addNewClassModal: enAddNewClassModalJson,
      settings: enSettingsJson,
      permissions: enPermissionsJson,
      personNavigation: enPersonNavigationJson,
      classIndex: enClassIndexJson,
      addNewSubjectModal: enAddNewSubjectModalJson,
      subjects: enSubjectsjson,
      peoplePicker: enPeoplePickerJson,
      manageTeachers: enManageTeachersJson,
      addNewTeacherModal: enAddNewTeacherModalJson,
      person: enPersonJson,
      educationCycle: enEducationCycleJson,
      service: enServiceJson,
      configureEducationCycleForm: enConfigureEducationCycleJson,
      educationCycles: enEducationCyclesJson,
      educationCyclesPicker: enEducationCyclesPickerJson,
      classPicker: enClassPickerJson,
    },
    pl: {
      common: plCommonJson,
      loginForm: plLoginFormJson,
      notifications: plNotificationsJson,
      registerForm: plRegisterFormJson,
      activateAccount: plActivateAccountJson,
      ActivateStudent: plActivateStudentJson,
      header: plHeaderJson,
      dashboardNavigation: plDashboardNavigationJson,
      addNewStudentModal: plAddNewStudentModalJson,
      studentsList: plStudentsListJson,
      profile: plProfileJson,
      invitations: plInvitationsJson,
      addInvitationModal: plAddInvitationModalJson,
      ActivateAdministrator: plActivateAdministrator,
      ActivateAdministratorPerson: plActivateAdministratorPersonJson,
      ActivateAdministratorSchool: plActivateAdministratorSchoolJson,
      schoolSelect: plSchoolSelectJson,
      schoolsList: plSchoolsListJson,
      classes: plClassesJson,
      addNewClassModal: plAddNewClassModalJson,
      settings: plSettingsJson,
      permissions: plPermissionsJson,
      personNavigation: plPersonNavigationJson,
      classIndex: plClassIndexJson,
      addNewSubjectModal: plAddNewSubjectModalJson,
      subjects: plSubjectsjson,
      peoplePicker: plPeoplePickerJson,
      manageTeachers: plManageTeachersJson,
      addNewTeacherModal: plAddNewTeacherModalJson,
      person: plPersonJson,
      educationCycle: plEducationCycleJson,
      service: plServiceJson,
      configureEducationCycleForm: plConfigureEducationCycleJson,
      educationCycles: plEducationCyclesJson,
      educationCyclesPicker: plEducationCyclesPickerJson,
      classPicker: plClassPickerJson,
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
