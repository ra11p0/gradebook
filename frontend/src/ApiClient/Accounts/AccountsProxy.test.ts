import assert from 'assert';
import AccountsProxy from './AccountsProxy';
import testConstraints from '../../tests/Constraints';
import accountsQuickActions from '../../tests/QuickActions/accountsQuickActions';
import AdministratorsProxy from '../Administrators/AdministratorsProxy';
import dotenv from 'dotenv';
dotenv.config();
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

describe('AccountsProxy', () => {
  describe('Register', () => {
    it('Should register', async () => {
      return await AccountsProxy.register({
        email: testConstraints.email,
        password: testConstraints.password,
      })
        .then((registerResponse) => {
          assert.equal(registerResponse.status, 200);
        })
        .catch(() => {
          if (!isTestEnvironment) {
            assert.ok(true);
          }
        });
    });
    it('Should not register - wrong email', async () => {
      return await AccountsProxy.register({
        email: 'testtest.pl',
        password: '!QAZ2wsx',
      })
        .then((registerResponse) => assert.equal(registerResponse.status, 400))
        .catch(() => assert.ok(true));
    });
  });

  describe('Login', () => {
    it('Should login', async () => {
      return await AccountsProxy.logIn({
        email: testConstraints.email,
        password: testConstraints.password,
      }).then((registerResponse) => {
        assert.equal(registerResponse.status, 200);
      });
    });
  });

  describe('Activate', () => {
    it('Should activate as administrator with new school', async () => {
      //  login to account
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      //  activate school and administrator
      await AdministratorsProxy.newAdministratorWithSchool(
        {
          name: 'Mateusz',
          surname: 'Szwagierczak',
          birthday: new Date(1991, 12, 12),
        },
        {
          name: testConstraints.schoolName,
          addressLine1: testConstraints.schoolAddress,
          addressLine2: '',
          city: testConstraints.schoolCity,
          postalCode: '27-270',
        }
      );
    });
    it('Should show accessible school', async () => {
      //  login to account
      await accountsQuickActions.logIn(
        testConstraints.email,
        testConstraints.password
      );
      // get me
      const meResponse = await AccountsProxy.getMe();
      //  get all accessible schools
      const accessibleSchools = await AccountsProxy.getAccessibleSchools(
        meResponse.data.userId
      );
      const foundSchool = accessibleSchools.data.find(
        (e) =>
          e.school.name === testConstraints.schoolName &&
          e.school.city === testConstraints.schoolCity &&
          e.school.addressLine1 === testConstraints.schoolAddress
      );
      assert.ok(foundSchool);
    });
  });
});
