import { REFRESH_USER } from "../Constraints/actionTypes";

const refreshUser = {
    type: REFRESH_USER
}
export interface refreshUserAction {
    roles: string[],
    userId: string,
    personGuid: string,
}

export const refreshUserWrapper = (dispatch: any, action: refreshUserAction) => dispatch({ ...refreshUser, ...action });