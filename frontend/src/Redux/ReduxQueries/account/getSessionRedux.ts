import moment, { Moment } from 'moment';
import { GlobalState, store } from '../../../store';

interface Session {
  accessToken: string;
  refreshToken: string;
  expiresIn: Moment;
}

export default (state: GlobalState = store.getState()): Session | undefined => {
  if (!state.common.session?.accessToken || !state.common.session.refreshToken)
    return undefined;
  return {
    accessToken: state.common.session.accessToken,
    refreshToken: state.common.session.refreshToken,
    expiresIn: moment(state.common.session.expiresIn),
  };
};
