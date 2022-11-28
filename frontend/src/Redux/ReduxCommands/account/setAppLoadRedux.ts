import ActionTypes from '../../ActionTypes/accountActionTypes';

const appLoad = {
  type: ActionTypes.AppLoad,
};
export default (dispatch: any, isAppLoaded: boolean): void =>
  dispatch({ ...appLoad, payload: { isAppLoaded } });
