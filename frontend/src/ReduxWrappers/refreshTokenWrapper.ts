import { REFRESH_TOKEN } from "../Constraints/actionTypes";

const refreshTokenAction = {
    type: REFRESH_TOKEN,
}

export interface refreshTokenAction {
    token: string;
    refreshToken: string;
}

export const refreshTokenWrapper = (dispatch: any, action: refreshTokenAction) => dispatch({ ...refreshTokenAction, ...action });
