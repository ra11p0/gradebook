import assert from "assert";
import testConstraints from '../../tests/Constraints'
import accountsQuickActions from "../../tests/QuickActions/accountsQuickActions";
import AdministratorsProxy from "../Administrators/AdministratorsProxy";
import Constraints from "../../tests/Constraints";
import AccountsProxy from "../Accounts/AccountsProxy";
import ClassesProxy from "./ClassesProxy";
import SchoolsProxy from "../Schools/SchoolsProxy";
require('dotenv').config();

const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('ClassesProxy', () => {
    describe('Classes', () => {
        it('Should not add student to class because already added to another', async () => {
            //  login to account
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            //  get user guid
            let me = await AccountsProxy.getMe();
            //  get schools
            let schoolsList = await AccountsProxy.getAccessibleSchools(me.data.userId);
            let schoolGuid = schoolsList.data.find(() => true)?.school.guid!;
            if (!schoolGuid) return assert.ok(false, "School not found!");
            let student = await SchoolsProxy.addNewStudent({
                Name: "Maria",
                Surname: "Sokołowska",
                Birthday: new Date(2007, 8, 28)
            }, schoolGuid);
            //  create new classes
            let class1 = await SchoolsProxy.addNewClass({
                name: '2TIC-1'
            }, schoolGuid);
            let class2 = await SchoolsProxy.addNewClass({
                name: '2TIC-2'
            }, schoolGuid);

            await ClassesProxy.addStudentsToClass(class1.data, [student.data]);
            return ClassesProxy.addStudentsToClass(class2.data, [student.data]).then((resp) =>
                assert.equal(resp.status, 400)
            ).catch(() =>
                assert.ok(true)
            );
        })
    })
})
