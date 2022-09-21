import { APP_LOAD, LOG_OUT, REFRESH_USER, SET_SCHOOL, SET_SCHOOLS_LIST } from '../../Constraints/actionTypes'

export const appLoad = {
    type: APP_LOAD,
    isAppLoaded: true
};
export const logOut = {
    type: LOG_OUT,
}
export const refreshUser = {
    type: REFRESH_USER
}
export const setSchool = {
    type: SET_SCHOOL
}
export const setSchoolsList = {
    type: SET_SCHOOLS_LIST
}
