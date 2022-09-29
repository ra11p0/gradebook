import assert from "assert";
import PeopleProxy from "./PeopleProxy";
import accountsQuickActions from '../../tests/QuickActions/accountsQuickActions';
import Constraints from "../../tests/Constraints";
import AccountsProxy from "../Accounts/AccountsProxy";
import PermissionEnum from "../../Common/Enums/Permissions/PermissionEnum";
import PermissionLevelEnum from "../../Common/Enums/Permissions/PermissionLevelEnum";
require('dotenv').config();


describe('PeopleProxy', () => {
    describe('Permissions', () => {
        it('Should set permission', async () => {
            await accountsQuickActions.logIn(Constraints.email, Constraints.password);
            var me = await AccountsProxy.getMe();
            var personGuid = (await AccountsProxy.getRelatedPeople(me.data.userId)).data.find(() => true);
            if (!personGuid) return assert.ok(false, "could not find person")
            await PeopleProxy.permissions.setPermissions(
                personGuid?.guid!,
                [
                    { permissionId: PermissionEnum.Invitations, permissionLevel: PermissionLevelEnum.Invitations_CannotInvite }
                ]
            );
            var permissionsResponse = await PeopleProxy.permissions.getPermissions(personGuid?.guid!);
            var permissions = permissionsResponse.data;
            if (permissions.find(e => e.permissionId == PermissionEnum.Invitations)?.permissionLevel != PermissionLevelEnum.Invitations_CannotInvite) return assert.ok(false, "could not set permission");


            await PeopleProxy.permissions.setPermissions(
                personGuid?.guid!,
                [
                    { permissionId: PermissionEnum.Invitations, permissionLevel: PermissionLevelEnum.Invitations_CanInvite }
                ]
            );

            permissionsResponse = await PeopleProxy.permissions.getPermissions(personGuid?.guid!);
            permissions = permissionsResponse.data;
            assert.equal(permissions.find(e => e.permissionId == PermissionEnum.Invitations)?.permissionLevel, PermissionLevelEnum.Invitations_CanInvite);
        })
    })

})
