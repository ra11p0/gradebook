import assert from "assert";
import testConstraints from '../../tests/Constraints'
import accountsQuickActions from "../../tests/QuickActions/accountsQuickActions";
import AdministratorsProxy from "../Administrators/AdministratorsProxy";
import Constraints from "../../tests/Constraints";
import AccountsProxy from "../Accounts/AccountsProxy";
import SchoolsProxy from './SchoolsProxy'
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";
import ClassesProxy from "../Classes/ClassesProxy";
require('dotenv').config();

const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('SchoolsProxy', () => {
    describe('School', () => {
        it('Should get school', async () => {
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let school = await SchoolsProxy.getSchool(accessibleSchools.data.find(() => true)?.school.guid!);
            assert.ok(school);
        })
        it('Should add person to school and show ', async () => {
            const newStudentName = 'Mateusz';
            const newStudentSurname = "Walczak";
            const newStudentBirthday = new Date(2002, 12, 21)
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let firstSchoolGuid = await accessibleSchools.data.find(() => true)?.school.guid!;
            SchoolsProxy.addNewStudent({
                Name: newStudentName,
                Surname: newStudentSurname,
                Birthday: newStudentBirthday
            }, firstSchoolGuid);
            let studentsInSchool = await SchoolsProxy.getStudentsInSchool(firstSchoolGuid, 1);
            let searchedStudent = studentsInSchool.data.find(e =>
                e.name == newStudentName &&
                e.surname == newStudentSurname &&
                e.schoolRole == SchoolRolesEnum.Student
            );
            assert.ok(searchedStudent);
        })
        it('Should add teacher to school and show ', async () => {
            const newTeacherName = 'Monika';
            const newTeacherSurname = "Klimończuk";
            const newTeacherBirthday = new Date(1991, 12, 21)
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let firstSchoolGuid = await accessibleSchools.data.find(() => true)?.school.guid!;
            SchoolsProxy.addNewTeacher({
                Name: newTeacherName,
                Surname: newTeacherSurname,
                Birthday: newTeacherBirthday
            }, firstSchoolGuid);
            let teachersInSchool = await SchoolsProxy.getTeachersInSchool(firstSchoolGuid, 1);
            let searchedTeacher = teachersInSchool.data.find(e =>
                e.name == newTeacherName &&
                e.surname == newTeacherSurname
            );
            assert.ok(searchedTeacher);
        })
    })
    describe('Classes', () => {
        it('Should create and see new class', async () => {
            const newClassName = "TI1";
            const newClassDescription = "Technik Informatyk 1";
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let firstSchoolGuid = await accessibleSchools.data.find(() => true)?.school.guid!;
            //  create new class 
            await SchoolsProxy.addNewClass({
                name: newClassName,
                description: newClassDescription
            }, firstSchoolGuid);
            //  get created class
            let classesInSchool = await SchoolsProxy.getClassesInSchool(firstSchoolGuid, 1);
            let foundClass = classesInSchool.data.find(e =>
                e.name == newClassName &&
                e.description == newClassDescription
            );
            assert.ok(foundClass);
        })
        it('Should add student to class', async () => {
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let firstSchoolGuid = await accessibleSchools.data.find(() => true)?.school.guid!;
            //  get created class
            let classesInSchool = await SchoolsProxy.getClassesInSchool(firstSchoolGuid, 1);
            let firstFoundClass = classesInSchool.data.find(() => true)!;
            let studentsInSchool = await SchoolsProxy.getStudentsInSchool(firstSchoolGuid);
            let firstStudentGuid = studentsInSchool.data.find(() => true)?.guid!;
            await ClassesProxy.addStudentToClass(firstFoundClass.guid, [firstStudentGuid]);
            var studentsInClass = await ClassesProxy.getStudentsInClass(firstFoundClass.guid);
            var foundStudent = studentsInClass.data.find(e => e.guid == firstStudentGuid);
            assert.ok(foundStudent);
        })
        it('Should add teacher to class', async () => {
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            let meResponse = await AccountsProxy.getMe();
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId);
            let firstSchoolGuid = await accessibleSchools.data.find(() => true)?.school.guid!;
            //  get created class
            let classesInSchool = await SchoolsProxy.getClassesInSchool(firstSchoolGuid, 1);
            let firstFoundClass = classesInSchool.data.find(() => true)!;
            let teachersInSchool = await SchoolsProxy.getTeachersInSchool(firstSchoolGuid);
            let firstTeacherGuid = teachersInSchool.data.find(() => true)?.guid!;
            await ClassesProxy.addTeacherToClass(firstFoundClass.guid, [firstTeacherGuid]);
            var teachersInClass = await ClassesProxy.getTeachersInClass(firstFoundClass.guid);
            var foundTeacher = teachersInClass.data.find(e => e.guid == firstTeacherGuid);
            assert.ok(foundTeacher);
        })
    })
})
