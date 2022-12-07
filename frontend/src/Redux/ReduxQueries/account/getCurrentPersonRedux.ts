import { GlobalState, store } from '../../../store';

export default (
  state: GlobalState = store.getState()
): CurrentPersonProxyResult | undefined => {
  const currentPersonGuid = state.common.person?.personGuid;
  if (!currentPersonGuid) return undefined;
  if (!state.common.schoolsList) throw new Error('schools list not set ');
  const schoolPerson = state.common.schoolsList.find(
    (schoolPerson: any) => schoolPerson.person.guid === currentPersonGuid
  );
  if (!schoolPerson)
    throw new Error('person not linked to any avaliable school');

  return schoolPerson.person;
};

export interface CurrentPersonProxyResult {
  guid: string;
  name: string;
  surname: string;
  schoolRole: number;
  birthday: Date;
}
