import assert from "assert";
import AccountsProxy from "../../ApiClient/Accounts/AccountsProxy";
import AdministratorsProxy from "../../ApiClient/Administrators/AdministratorsProxy";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import PeopleProxy from "../../ApiClient/People/PeopleProxy"
import ClassesProxy from "../../ApiClient/Classes/ClassesProxy"
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";
import testConstraints from '../Constraints'
import accountsQuickActions from "../QuickActions/accountsQuickActions";
require('dotenv').config();

const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('Integration', () => {
    describe('Classes', () => {
        it('Should show only avalible students', async () => {
            //  login to account
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            //  get user guid
            let me = await AccountsProxy.getMe();
            //  get schools
            let schoolsList = await AccountsProxy.getAccessibleSchools(me.data.userId);
            let schoolGuid = schoolsList.data.map(s => s.school.guid).find(() => true);
            if (!schoolGuid) return assert.ok(false, "School not found!");
            let student1 = await SchoolsProxy.addNewStudent({
                Name: "Michał",
                Surname: "Karolak",
                Birthday: new Date(2001, 2, 3)
            }, schoolGuid);
            let student2 = await SchoolsProxy.addNewStudent({
                Name: "Janek",
                Surname: "Walecki",
                Birthday: new Date(2004, 7, 14)
            }, schoolGuid);
            let student3 = await SchoolsProxy.addNewStudent({
                Name: "Konrad",
                Surname: "Struzik",
                Birthday: new Date(2006, 1, 20)
            }, schoolGuid);
            let student4 = await SchoolsProxy.addNewStudent({
                Name: "Michał",
                Surname: "Stankiewicz",
                Birthday: new Date(2001, 11, 23)
            }, schoolGuid);

            //  create new classes
            let class1 = await SchoolsProxy.addNewClass({
                name: '1TIC-1'
            }, schoolGuid);
            let class2 = await SchoolsProxy.addNewClass({
                name: '1TIC-2'
            }, schoolGuid);

            //  add students to classes

            await ClassesProxy.addStudentsToClass(class1.data, [student1.data, student2.data]);
            await ClassesProxy.addStudentsToClass(class2.data, [student3.data]);

            let searchResult = await ClassesProxy.searchStudentsCandidatesToClassWithCurrent(class1.data, "", 0);
            let mappedSearchResult = searchResult.data.map(s => s.guid);
            if (!(mappedSearchResult.includes(student1.data) &&
                mappedSearchResult.includes(student2.data) &&
                mappedSearchResult.includes(student4.data)))
                return assert.ok(false, "Wrong search result!");
            if (mappedSearchResult.includes(student3.data))
                return assert.ok(false, "Wrong search result!");
        })
    })

})
