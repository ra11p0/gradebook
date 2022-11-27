import assert from 'assert';
import testConstraints from '../../tests/Constraints';
import accountsQuickActions from '../../tests/QuickActions/accountsQuickActions';
import PermissionEnum from '../../Common/Enums/Permissions/PermissionEnum';
import AccountsProxy from '../Accounts/AccountsProxy';
import ClassesProxy from './ClassesProxy';
import SchoolsProxy from '../Schools/SchoolsProxy';
import PeopleProxy from '../People/PeopleProxy';
import getCurrentPersonReduxProxy from '../../Redux/ReduxQueries/account/getCurrentPersonRedux';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import dotenv from 'dotenv';

dotenv.config();

describe('ClassesProxy', () => {
  describe('Classes', () => {
    it('Should not add student to class because already added to another', async () => {
      //  login to account
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      //  get user guid
      const me = await AccountsProxy.getMe();
      //  set permission to add new student
      await PeopleProxy.permissions.setPermissions(
        getCurrentPersonReduxProxy()!.guid,
        [
          {
            permissionId: PermissionEnum.Students,
            permissionLevel: PermissionLevelEnum.Students_CanCreateAndDelete,
          },
        ]
      );
      //  get schools
      const schoolsList = await AccountsProxy.getAccessibleSchools(
        me.data.userId
      );
      const schoolGuid = schoolsList.data.find(() => true)?.school.guid;
      if (!schoolGuid) return assert.ok(false, 'School not found!');
      const student = await SchoolsProxy.addNewStudent(
        {
          Name: 'Maria',
          Surname: 'Sokołowska',
          Birthday: new Date(2007, 8, 28),
        },
        schoolGuid
      );
      //  create new classes
      const class1 = await SchoolsProxy.addNewClass(
        {
          name: '2TIC-1',
        },
        schoolGuid
      );
      const class2 = await SchoolsProxy.addNewClass(
        {
          name: '2TIC-2',
        },
        schoolGuid
      );

      await ClassesProxy.addStudentsToClass(class1.data, [student.data]);
      return await ClassesProxy.addStudentsToClass(class2.data, [student.data])
        .then((resp) => assert.equal(resp.status, 400))
        .catch(() => assert.ok(true));
    });
  });
});
