import assert from "assert";
import AccountsProxy from "../../../src/ApiClient/Accounts/AccountsProxy";
require('dotenv').config();

describe('Accounts', () => {
    describe('Register', () => {
        it('Should register', () => {
            return AccountsProxy.register({
                email: "test@test.pl",
                password: "!QAZ2wsx"
            }).then(registerResponse => assert.equal(registerResponse.status, 200));
        })
        it('Should not register - wrong email', () => {
            return AccountsProxy.register({
                email: "testtest.pl",
                password: "!QAZ2wsx"
            })
                .then(registerResponse => assert.equal(registerResponse.status, 400))
                .catch(error => assert.equal(error.response.status, 400));
        })
    })

    describe('Login', () => {
        it('Should login', () => {
            return AccountsProxy.logIn({
                username: "test@test.pl",
                password: "!QAZ2wsx",
            }).then(registerResponse => {
                assert.equal(registerResponse.status, 200)
            })
        })
    })

})
