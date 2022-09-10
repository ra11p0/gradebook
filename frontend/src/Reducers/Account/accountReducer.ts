import { APP_LOAD, LOG_IN, LOG_OUT, REFRESH_TOKEN, REFRESH_USER, SET_SCHOOL, SET_SCHOOLS_LIST } from '../../Constraints/actionTypes'

const defaultState = {
    appLoaded: false,
    isLoggedIn: false
};

export default (state: any = defaultState, action: any) => {
    switch (action.type) {
        case APP_LOAD:
            return {
                ...state,
                appLoaded: action.isAppLoaded,
            };
        case LOG_IN:
            return {
                ...state,
                isLoggedIn: true,
                session: {
                    token: action.token,
                    refreshToken: action.refreshToken,
                    username: action.username,
                    userId: action.userId,
                    personGuid: action.personGuid,
                    roles: action.roles
                }
            };
        case LOG_OUT:
            localStorage.removeItem('access_token');
            localStorage.removeItem('refresh');
            return {
                ...state,
                isLoggedIn: false,
                session: null,
                school: null,
                schoolsList: null
            };
        case REFRESH_TOKEN:
            localStorage.setItem('access_token', action.token);
            localStorage.setItem('refresh', action.refreshToken);
            return {
                ...state,
                isLoggedIn: true,
                session: {
                    ...state.session,
                    token: action.token,
                    refreshToken: action.refreshToken
                }
            };
        case REFRESH_USER:
            return {
                ...state,
                isLoggedIn: true,
                session: {
                    ...state.session,
                    roles: action.roles,
                    userId: action.userId,
                    personGuid: action.personGuid,
                }
            };
        case SET_SCHOOL:
            return {
                ...state,
                school: {
                    ...state.school,
                    schoolGuid: action.schoolGuid,
                    schoolName: action.schoolName
                }
            };
        case SET_SCHOOLS_LIST:
            return {
                ...state,
                schoolsList: action.schoolsList
            };
        default:
            return state;
    }
}
