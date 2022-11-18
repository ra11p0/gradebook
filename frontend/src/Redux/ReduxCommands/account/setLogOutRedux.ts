import { store } from "../../../store";
import ActionTypes from "../../ActionTypes/accountActionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

export default (dispatch: any = store.dispatch) => {

    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh');

    dispatch({ type: ActionTypes.LogOut })
};
