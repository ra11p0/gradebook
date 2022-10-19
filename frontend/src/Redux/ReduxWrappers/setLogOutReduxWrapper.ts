import { LOG_OUT } from "../../Constraints/actionTypes";
import { store } from "../../store";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

const logOut = {
    type: LOG_OUT,
}

export default (dispatch: any = store.dispatch) => {

    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh');

    dispatch({ ...logOut })
};
