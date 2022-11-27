import assert from 'assert';
import testConstraints from '../../tests/Constraints';
import accountsQuickActions from '../../tests/QuickActions/accountsQuickActions';
import AccountsProxy from '../Accounts/AccountsProxy';
import SchoolsProxy from './SchoolsProxy';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import ClassesProxy from '../Classes/ClassesProxy';
import dotenv from 'dotenv';
dotenv.config();

describe('SchoolsProxy', () => {
  describe('School', () => {
    it('Should get school', async () => {
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const foundSchool = accessibleSchools.data.find(() => true)?.school.guid;
      if (!foundSchool) throw new Error('could not find school');
      const school = await SchoolsProxy.getSchool(foundSchool);
      assert.ok(school);
    });
    it('Should add student to school and show ', async () => {
      const newStudentName = 'Mateusz';
      const newStudentSurname = 'Walczak';
      const newStudentBirthday = new Date(2002, 12, 21);
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const firstSchoolGuid = await accessibleSchools.data.find(() => true)
        ?.school.guid;
      if (!firstSchoolGuid) assert.ok(false, 'no schools');
      await SchoolsProxy.addNewStudent(
        {
          Name: newStudentName,
          Surname: newStudentSurname,
          Birthday: newStudentBirthday,
        },
        firstSchoolGuid
      );
      const studentsInSchool = await SchoolsProxy.getStudentsInSchool(
        firstSchoolGuid,
        0
      );
      const searchedStudent = studentsInSchool.data.find(
        (e) =>
          e.name === newStudentName &&
          e.surname === newStudentSurname &&
          e.schoolRole === SchoolRolesEnum.Student
      );
      assert.ok(searchedStudent);
    });
    it('Should add teacher to school and show ', async () => {
      const newTeacherName = 'Monika';
      const newTeacherSurname = 'Klimończuk';
      const newTeacherBirthday = new Date(1991, 12, 21);
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const firstSchoolGuid = await accessibleSchools.data.find(() => true)
        ?.school.guid;
      if (!firstSchoolGuid) assert.ok(false, 'no schools');
      await SchoolsProxy.addNewTeacher(
        {
          Name: newTeacherName,
          Surname: newTeacherSurname,
          Birthday: newTeacherBirthday,
        },
        firstSchoolGuid
      );
      const teachersInSchool = await SchoolsProxy.getTeachersInSchool(
        firstSchoolGuid,
        0
      );
      const searchedTeacher = teachersInSchool.data.find(
        (e) => e.name === newTeacherName && e.surname === newTeacherSurname
      );
      assert.ok(searchedTeacher);
    });
  });
  describe('Classes', () => {
    it('Should create and see new class', async () => {
      const newClassName = 'TI1';
      const newClassDescription = 'Technik Informatyk 1';
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const firstSchoolGuid = await accessibleSchools.data.find(() => true)
        ?.school.guid;
      if (!firstSchoolGuid) throw new Error('school not found');
      //  create new class
      await SchoolsProxy.addNewClass(
        {
          name: newClassName,
          description: newClassDescription,
        },
        firstSchoolGuid
      );
      //  get created class
      const classesInSchool = await SchoolsProxy.getClassesInSchool(
        firstSchoolGuid,
        1
      );
      const foundClass = classesInSchool.data.find(
        (e) => e.name === newClassName && e.description === newClassDescription
      );
      assert.ok(foundClass);
    });
    it('Should add student to class', async () => {
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const firstSchoolGuid = await accessibleSchools.data.find(() => true)
        ?.school.guid;
      if (!firstSchoolGuid) throw new Error('school not found');
      //  get created class
      const classesInSchool = await SchoolsProxy.getClassesInSchool(
        firstSchoolGuid,
        1
      );
      const firstFoundClass = classesInSchool.data.find(() => true)!;
      const student = await SchoolsProxy.addNewStudent(
        {
          Name: 'Maria',
          Surname: 'Sokołowska',
          Birthday: new Date(2007, 8, 28),
        },
        firstSchoolGuid
      );

      const firstStudentGuid = student.data;
      await ClassesProxy.addStudentsToClass(firstFoundClass.guid, [
        firstStudentGuid,
      ]);
      const studentsInClass = await ClassesProxy.getStudentsInClass(
        firstFoundClass.guid
      );
      const foundStudent = studentsInClass.data.find(
        (e) => e.guid === firstStudentGuid
      );
      assert.ok(foundStudent);
    });
    it('Should add teacher to class', async () => {
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      const meResponse = await AccountsProxy.getMe();
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const firstSchoolGuid = await accessibleSchools.data.find(() => true)
        ?.school.guid;
      if (!firstSchoolGuid) throw new Error('school not found');
      //  get created class
      const classesInSchool = await SchoolsProxy.getClassesInSchool(
        firstSchoolGuid,
        0
      );
      const firstFoundClass = classesInSchool.data.find(() => true)!;
      const teachersInSchool = await SchoolsProxy.getTeachersInSchool(
        firstSchoolGuid
      );

      const firstTeacherGuid = teachersInSchool.data.find(() => true)?.guid;
      if (!firstTeacherGuid) throw new Error('teacher not found');
      await ClassesProxy.addTeachersToClass(firstFoundClass.guid, [
        firstTeacherGuid,
      ]);
      const teachersInClass = await ClassesProxy.getTeachersInClass(
        firstFoundClass.guid
      );
      const foundTeacher = teachersInClass.data.find(
        (e) => e.guid === firstTeacherGuid
      );
      assert.ok(foundTeacher);
    });
  });
});
