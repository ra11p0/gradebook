import assert from "assert";
import AccountsProxy from "../../ApiClient/Accounts/AccountsProxy";
import AdministratorsProxy from "../../ApiClient/Administrators/AdministratorsProxy";
import SchoolsProxy from "../../ApiClient/Schools/SchoolsProxy";
import PeopleProxy from "../../ApiClient/People/PeopleProxy"
import SchoolRolesEnum from "../../Common/Enums/SchoolRolesEnum";
import testConstraints from '../Constraints'
import accountsQuickActions from "../QuickActions/accountsQuickActions";

describe('Integration', () => {
    describe('Account', () => {
        it('Should invite and activate new student', async () => {
            //  set up
            const newStudentName = "Maria";
            const newStudentSurname = "Gontarczyk";
            const newStudentBirthday = new Date(2004, 12, 12);
            const newStudentEmail = "maria.gontarczyk@school.pl";

            //  login to account
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            //  get user guid
            let me = await AccountsProxy.getMe();
            //  get schools
            let schoolsList = await AccountsProxy.getAccessibleSchools(me.data.userId);
            let schoolGuid = schoolsList.data.find(() => true)?.school.guid!;
            if (!schoolGuid) return assert.ok(false, "School not found!");
            //  add new student to first school
            await SchoolsProxy.addNewStudent({
                Name: newStudentName,
                Surname: newStudentSurname,
                Birthday: newStudentBirthday
            }, schoolGuid);
            //  get students list
            var students = await SchoolsProxy.getInactiveAccessibleStudentsInSchool(schoolGuid);
            //TODO: Check dates!
            var student = students.data.find(e => e.name === newStudentName && e.surname === newStudentSurname && e.isActive === false);
            if (!student) return assert.ok(false, "Student not found!");
            //  invite student
            let activationCodes = await SchoolsProxy.inviteMultiplePeople({
                invitedPersonGuidArray: [student.guid],
                role: SchoolRolesEnum.Student
            }, schoolGuid);
            let activationCode = activationCodes.data.find(() => true);
            if (!activationCode) return assert.ok(false, "Invitation not created!");
            //  log out
            accountsQuickActions.logOut();
            //  register student
            await AccountsProxy.register({
                email: newStudentEmail,
                password: testConstraints.password
            });
            await accountsQuickActions.logIn(newStudentEmail, testConstraints.password);
            await PeopleProxy.activatePerson(activationCode);
            let newMe = await AccountsProxy.getMe();
            return assert.notEqual(newMe.data.schools.length, 0)
        })
    })

})
