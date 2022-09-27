import assert from "assert";
import { waitForDebugger } from "inspector";
import AccountsProxy from "../../../src/ApiClient/Accounts/AccountsProxy";

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
            }).then(registerResponse => assert.equal(registerResponse.status, 400));
        })
    })

    describe('Login', () => {
        it('Should login', () => {
            return AccountsProxy.logIn({
                username: "test@test.pl",
                password: "!QAZ2wsx",
            }).then(registerResponse => {
                console.log(registerResponse.data);
                assert.equal(registerResponse.status, 200)
            });
        })
    })

})
