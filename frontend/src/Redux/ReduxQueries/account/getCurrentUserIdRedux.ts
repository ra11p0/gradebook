import { store } from '../../../store';

export default (state: any = store.getState()): string =>
  state.common.user?.userId;
