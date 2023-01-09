import { disconnectAllHubs } from '../../../ApiClient/SignalR/HubsResolver';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';

export default async (dispatch: any = store.dispatch): Promise<void> => {
  localStorage.removeItem('access_token');
  localStorage.removeItem('refresh_token');
  localStorage.removeItem('expires_in');
  await disconnectAllHubs();

  dispatch({ type: ActionTypes.LogOut });
};
