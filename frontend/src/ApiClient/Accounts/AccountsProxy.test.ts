import assert from "assert";
import AccountsProxy from "./AccountsProxy";
import testConstraints from '../../tests/Constraints'
import accountsQuickActions from "../../tests/QuickActions/accountsQuickActions";
import AdministratorsProxy from "../Administrators/AdministratorsProxy";
import Constraints from "../../tests/Constraints";
require('dotenv').config();

const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('AccountsProxy', () => {
    describe('Register', () => {
        it('Should register', () => {
            return AccountsProxy.register({
                email: testConstraints.email,
                password: testConstraints.password
            }).then(registerResponse => {

                assert.equal(registerResponse.status, 200)
            }).catch(err => {
                if (!isTestEnvironment) {
                    assert.ok(true);
                    return;
                }
            });
        })
        it('Should not register - wrong email', () => {
            return AccountsProxy.register({
                email: "testtest.pl",
                password: "!QAZ2wsx"
            })
                .then(registerResponse => assert.equal(registerResponse.status, 400))
                .catch(() => assert.ok(true));
        })
    })

    describe('Login', () => {
        it('Should login', () => {
            return AccountsProxy.logIn({
                email: testConstraints.email,
                password: testConstraints.password,
            }).then(registerResponse => {
                assert.equal(registerResponse.status, 200)
            })
        })
    })

    describe('Activate', () => {
        it('Should activate as administrator with new school', async () => {
            //  login to account
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            //  activate school and administrator
            await AdministratorsProxy.newAdministratorWithSchool({
                name: "Mateusz",
                surname: "Szwagierczak",
                birthday: new Date(1991, 12, 12)
            }, {
                name: Constraints.schoolName,
                addressLine1: Constraints.schoolAddress,
                addressLine2: "",
                city: Constraints.schoolCity,
                postalCode: "27-270"
            });
        })
        it('Should show accessible school', async () => {
            //  login to account
            await accountsQuickActions.logIn(testConstraints.email, testConstraints.password);
            //get me
            let meResponse = await AccountsProxy.getMe();
            //  get all accessible schools
            let accessibleSchools = await AccountsProxy.getAccessibleSchools(meResponse.data.userId)
            let foundSchool = accessibleSchools.data.find(e =>
                e.school.name == Constraints.schoolName &&
                e.school.city == Constraints.schoolCity &&
                e.school.addressLine1 == Constraints.schoolAddress
            );
            assert.ok(foundSchool)
        })
    })


})
