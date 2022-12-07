import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';
import getPeopleReduxProxy from '../../ReduxQueries/account/getPeopleRedux';
import getSchoolsListRedux from '../../ReduxQueries/account/getSchoolsListRedux';
import setPersonReduxWrapper from './setPersonRedux';

export const setSchool = {
  type: ActionTypes.SetSchool,
};
export interface setSchoolAction {
  schoolName: string;
  schoolGuid: string;
}

export default async (
  schoolGuid: string,
  dispatch: any = store.dispatch
): Promise<void> => {
  const school = getSchoolsListRedux()?.find((e) => e.guid === schoolGuid);
  if (!school) throw new Error('school is undefined');
  const schoolAction: setSchoolAction = {
    schoolName: school.name,
    schoolGuid: school.guid,
  };

  dispatch({ ...setSchool, payload: { ...schoolAction } });

  const schoolRelatedPerson = getPeopleReduxProxy(store.getState()).find(
    (e) => e.schoolGuid === schoolGuid
  );

  if (!schoolRelatedPerson)
    throw new Error('school related person was undefined');

  await setPersonReduxWrapper(dispatch, {
    personGuid: schoolRelatedPerson?.guid ?? '',
    fullName: `${schoolRelatedPerson?.name} ${schoolRelatedPerson?.surname}`,
  });
};
