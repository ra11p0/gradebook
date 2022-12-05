import { GlobalState, store } from '../../../store';

export default (state: GlobalState = store.getState()): string => {
  return state.common.language ?? 'en';
};
