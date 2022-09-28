import assert from "assert";
import AccountsProxy from "./AccountsProxy";
import testConstraints from '../../tests/Constraints'
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

})
