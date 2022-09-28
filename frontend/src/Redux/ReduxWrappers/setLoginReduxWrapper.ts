import { LOG_IN } from "../../Constraints/actionTypes";
const isTestEnvironment = process.env.ENVIRONMENT === 'TEST';

const logIn = {
    type: LOG_IN
}

export interface logInAction {
    accessToken: string;
    refreshToken: string;
}

export default (dispatch: any, action: logInAction) => {
    if (!isTestEnvironment) {
        localStorage.setItem("access_token", action.accessToken);
        localStorage.setItem("refresh_token", action.refreshToken);
    }
    dispatch({ ...logIn, ...action });
};
