import { LOG_OUT } from "../../Constraints/actionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

const logOut = {
    type: LOG_OUT,
}

export default (dispatch: any) => {
    if (!isTestEnvironment) {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh');
    }
    dispatch({ ...logOut })
};
