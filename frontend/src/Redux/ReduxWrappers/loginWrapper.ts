import { LOG_IN } from "../../Constraints/actionTypes";

const logIn = {
    type: LOG_IN
}

export interface logInAction {
    accessToken: string;
    refreshToken: string;
}

export const loginWrapper = (dispatch: any, action: logInAction) => {
    localStorage.setItem("access_token", action.accessToken);
    localStorage.setItem("refresh_token", action.refreshToken);
    dispatch({ ...logIn, ...action });
};
