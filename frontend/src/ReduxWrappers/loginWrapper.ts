import { LOG_IN } from "../Constraints/actionTypes";

const logIn = {
    type: LOG_IN,
    isLoggedIn: true
}

export interface logInAction {
    access_token: string;
    refreshToken: string;
    username: string;
    userId: string;
    personGuid: string;
    name: string;
    surname: string;
    roles: string[]
}

export const loginWrapper = (dispatch: any, action: logInAction) => dispatch({ ...logIn, ...action });
