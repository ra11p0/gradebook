import PeopleProxy from '../../../ApiClient/People/PeopleProxy';
import ActionTypes from '../../ActionTypes/accountActionTypes';
import setPermissionsReduxWrapper from './setPermissionsRedux';

export const setPerson = {
  type: ActionTypes.SetPerson,
};

export interface setPersonAction {
  personGuid: string;
  fullName: string;
}

export default async (
  dispatch: any,
  action: setPersonAction
): Promise<void> => {
  dispatch({ ...setPerson, payload: { ...action } });
  const permissionsResponse = await PeopleProxy.permissions.getPermissions(
    action.personGuid
  );

  setPermissionsReduxWrapper(dispatch, {
    permissions: permissionsResponse.data.map((e) => e.permissionLevel),
  });
};
