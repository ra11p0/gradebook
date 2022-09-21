import { logIn } from '../Actions/Account/accountActions';

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
