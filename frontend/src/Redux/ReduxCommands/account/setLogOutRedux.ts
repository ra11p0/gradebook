import { disconnectAllHubs } from '../../../ApiClient/SignalR/HubsResolver';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';

export default async (dispatch: any = store.dispatch): Promise<void> => {
  localStorage.removeItem('access_token');
  localStorage.removeItem('refresh_token');

  await disconnectAllHubs();

  dispatch({ type: ActionTypes.LogOut });
};
