import { LOG_IN } from "../Constraints/actionTypes";

const logIn = {
    type: LOG_IN
}

export interface logInAction {
    accessToken: string;
    refreshToken: string;
}

export const loginWrapper = (dispatch: any, action: logInAction) => {
    dispatch({ ...logIn, ...action });
};
