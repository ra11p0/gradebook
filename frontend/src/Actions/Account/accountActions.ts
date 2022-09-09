import { APP_LOAD, LOG_IN, LOG_OUT, REFRESH_TOKEN, REFRESH_USER, SET_SCHOOL } from '../../Constraints/actionTypes'

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
export const refreshToken = {
    type: REFRESH_TOKEN,
}
export const refreshUser = {
    type: REFRESH_USER
}
export const setSchool = {
    type: SET_SCHOOL
}
