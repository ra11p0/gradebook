import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';

export default (dispatch: any = store.dispatch): void => {
  localStorage.removeItem('access_token');
  localStorage.removeItem('refresh');

  dispatch({ type: ActionTypes.LogOut });
};
