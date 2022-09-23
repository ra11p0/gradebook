import { SET_USER } from "../../Constraints/actionTypes";


export const setUser = {
    type: SET_USER
}

export interface setUserAction {
    userId: string;
}

export const setUserWrapper = (dispatch: any, action: setUserAction) => dispatch({ ...setUser, ...action });
