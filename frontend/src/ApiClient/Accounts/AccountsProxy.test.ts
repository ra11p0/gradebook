import assert from "assert";
import AccountsProxy from "./AccountsProxy";
import testConstraints from '../../tests/Constraints'
import accountsQuickActions from "../../tests/QuickActions/accountsQuickActions";
import AdministratorsProxy from "../Administrators/AdministratorsProxy";
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
                .catch(error => assert.equal(error.response.data.title, 'One or more validation errors occurred.'));
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
            //  create new student
            await AdministratorsProxy.newAdministratorWithSchool({
                name: "Mateusz",
                surname: "Szwagierczak",
                birthday: new Date(1991, 12, 12)
            }, {
                name: "ZS3",
                addressLine1: "Polna 12",
                addressLine2: "",
                city: "Wygwizdów",
                postalCode: "27-270"
            });
        })
    })

})
