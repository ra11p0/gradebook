import { store } from '../../../store';

export default (
  state: any = store.getState()
): getCurrentSchoolReduxProxyResult | undefined => {
  if (state.common?.school === undefined) {
    if (state.common?.schoolsList && state.common?.schoolsList?.length !== 0) {
      return {
        schoolName: state.common.schoolsList[0].school.name,
        schoolGuid: state.common.schoolsList[0].school.guid,
      };
    }
    return undefined;
  }
  return state.common?.school;
};

interface getCurrentSchoolReduxProxyResult {
  schoolName: string;
  schoolGuid: string;
}
