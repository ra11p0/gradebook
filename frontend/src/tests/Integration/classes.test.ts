import assert from 'assert';
import AccountsProxy from '../../ApiClient/Accounts/AccountsProxy';
import SchoolsProxy from '../../ApiClient/Schools/SchoolsProxy';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import testConstraints from '../Constraints';
import accountsQuickActions from '../QuickActions/accountsQuickActions';
import dotenv from 'dotenv';
dotenv.config();

describe('Integration', () => {
  describe('Classes', () => {
    it('Should show only avalible students', async () => {
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
      const schoolGuid = schoolsList.data
        .map((s) => s.school.guid)
        .find(() => true);
      if (!schoolGuid) return assert.ok(false, 'School not found!');
      const student1 = await SchoolsProxy.addNewStudent(
        {
          Name: 'Michał',
          Surname: 'Karolak',
          Birthday: new Date(2001, 2, 3),
        },
        schoolGuid
      );
      const student2 = await SchoolsProxy.addNewStudent(
        {
          Name: 'Janek',
          Surname: 'Walecki',
          Birthday: new Date(2004, 7, 14),
        },
        schoolGuid
      );
      const student3 = await SchoolsProxy.addNewStudent(
        {
          Name: 'Konrad',
          Surname: 'Struzik',
          Birthday: new Date(2006, 1, 20),
        },
        schoolGuid
      );
      const student4 = await SchoolsProxy.addNewStudent(
        {
          Name: 'Michał',
          Surname: 'Stankiewicz',
          Birthday: new Date(2001, 11, 23),
        },
        schoolGuid
      );

      //  create new classes
      const class1 = await SchoolsProxy.addNewClass(
        {
          name: '1TIC-1',
        },
        schoolGuid
      );
      const class2 = await SchoolsProxy.addNewClass(
        {
          name: '1TIC-2',
        },
        schoolGuid
      );

      //  add students to classes

      await ClassesProxy.addStudentsToClass(class1.data, [
        student1.data,
        student2.data,
      ]);
      await ClassesProxy.addStudentsToClass(class2.data, [student3.data]);

      const searchResult =
        await ClassesProxy.searchStudentsCandidatesToClassWithCurrent(
          class1.data,
          '',
          0
        );
      const mappedSearchResult = searchResult.data.map((s) => s.guid);
      if (
        !(
          mappedSearchResult.includes(student1.data) &&
          mappedSearchResult.includes(student2.data) &&
          mappedSearchResult.includes(student4.data)
        )
      )
        return assert.ok(false, 'Wrong search result!');
      if (mappedSearchResult.includes(student3.data))
        return assert.ok(false, 'Wrong search result!');
    });
  });
});
