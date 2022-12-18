import { store } from '../../../store';

export default (
  state: any = store.getState()
): GetCurrentSchoolReduxProxyResult | undefined => {
  return state.common?.school;
};

export interface GetCurrentSchoolReduxProxyResult {
  schoolName: string;
  schoolGuid: string;
}
