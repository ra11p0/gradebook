import ActionTypes from '../../ActionTypes/accountActionTypes';

export const setUser = {
  type: ActionTypes.SetUser,
};

export interface setUserAction {
  userId: string;
}

export default (dispatch: any, action: setUserAction): void =>
  dispatch({ ...setUser, payload: { ...action } });
