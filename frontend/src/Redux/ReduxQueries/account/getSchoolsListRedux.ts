import GetSchoolResponse from '../../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse';
import { GlobalState, store } from '../../../store';

export default (
  state: GlobalState = store.getState()
): GetSchoolResponse[] | undefined => {
  return state.common.schoolsList?.map((e: any) => e.school);
};
