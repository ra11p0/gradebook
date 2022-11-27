import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';
import getPeopleReduxProxy from '../../ReduxQueries/account/getPeopleRedux';
import setPersonReduxWrapper from './setPersonRedux';

export const setSchool = {
  type: ActionTypes.SetSchool,
};
export interface setSchoolAction {
  schoolGuid: string;
  schoolName: string;
}

export default async (
  dispatch: any,
  action: setSchoolAction
): Promise<void> => {
  dispatch({ ...setSchool, payload: { ...action } });
  const schoolRelatedPerson = getPeopleReduxProxy(store.getState()).find(
    (e) => e.schoolGuid === action.schoolGuid
  );
  if (!schoolRelatedPerson)
    throw new Error('school related person was undefined');
  await setPersonReduxWrapper(dispatch, {
    personGuid: schoolRelatedPerson?.guid ?? '',
    fullName: `${schoolRelatedPerson?.name} ${schoolRelatedPerson?.surname}`,
  });
};
