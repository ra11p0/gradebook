import { APP_LOAD, LOG_IN, LOG_OUT } from '../../Constraints/actionTypes'

export const appLoad = {
    type: APP_LOAD,
    isAppLoaded: true
};
export const logIn = {
    type: LOG_IN,
    isLoggedIn: true
}
export const logOut = {
    type: LOG_OUT,
}
