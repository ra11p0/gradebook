import assert from 'assert';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import SchoolsProxy from '../../ApiClient/Schools/SchoolsProxy';
import PeopleProxy from '../../ApiClient/People/PeopleProxy';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import testConstraints from '../Constraints';
import accountsQuickActions from '../QuickActions/accountsQuickActions';
import dotenv from 'dotenv';
dotenv.config();

const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('Integration', () => {
  describe('Account', () => {
    it('Should invite and activate new student', async () => {
      //  set up
      const newStudentName = 'Maria';
      const newStudentSurname = 'Gontarczyk';
      const newStudentBirthday = new Date(2004, 12, 12);
      const newStudentEmail = 'maria.gontarczyk@school.pl';

      //  login to account
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      //  get user guid
      const me = await AccountsProxy.getMe();
      //  get schools
      const schoolsList = await AccountsProxy.getAccessibleSchools(
        me.data.userId
      );
      const schoolGuid = schoolsList.data.find(() => true)?.school.guid;
      if (!schoolGuid) throw new Error('school guid was undefined');
      if (!schoolGuid) return assert.ok(false, 'School not found!');
      //  add new student to first school
      await SchoolsProxy.addNewStudent(
        {
          Name: newStudentName,
          Surname: newStudentSurname,
          Birthday: newStudentBirthday,
        },
        schoolGuid
      );
      //  get students list
      const students = await SchoolsProxy.getInactiveAccessibleStudentsInSchool(
        schoolGuid
      );
      // TODO: Check dates!
      const student = students.data.find(
        (e) =>
          e.name === newStudentName &&
          e.surname === newStudentSurname &&
          !e.isActive
      );
      if (!student) return assert.ok(false, 'Student not found!');
      //  invite student
      const activationCodes = await SchoolsProxy.inviteMultiplePeople(
        {
          invitedPersonGuidArray: [student.guid],
          role: SchoolRolesEnum.Student,
        },
        schoolGuid
      );
      const activationCode = activationCodes.data.find(() => true);
      if (!activationCode) return assert.ok(false, 'Invitation not created!');
      //  log out
      accountsQuickActions.logOut();
      //  register student
      try {
        await AccountsProxy.register({
          email: newStudentEmail,
          password: testConstraints.password,
        });
      } catch {
        if (isTestEnvironment) {
          return assert.ok(false, 'could not register new account');
        }
      }
      await accountsQuickActions.logIn(
        newStudentEmail,
        testConstraints.password
      );
      await PeopleProxy.activatePerson(activationCode);
      const newMe = await AccountsProxy.getMe();
      return assert.notEqual(newMe.data.schools.length, 0);
    });
  });
});
