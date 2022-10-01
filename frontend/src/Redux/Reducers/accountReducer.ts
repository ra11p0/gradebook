import { APP_LOAD, LOG_IN, LOG_OUT, SET_PERMISSIONS, SET_PERSON, SET_SCHOOL, SET_SCHOOLS_LIST, SET_USER } from '../../Constraints/actionTypes'

const defaultState = {
    appLoaded: false,
};

export default (state: any = defaultState, action: any) => {
    switch (action.type) {
        case APP_LOAD:
            return {
                ...state,
                appLoaded: action.isAppLoaded,
            };
        case SET_USER:
            return {
                ...state,
                user: {
                    userId: action.userId,
                }
            }
        case SET_PERSON:
            return {
                ...state,
                person: {
                    personGuid: action.personGuid,
                }
            }
        case LOG_IN:
            return {
                ...state,
                session: {
                    accessToken: action.accessToken,
                    refreshToken: action.refreshToken,
                }
            };
        case LOG_OUT:
            return {
                ...state,
                session: null,
                school: null,
                schoolsList: null,
                user: null
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
        case SET_PERMISSIONS:
            return {
                ...state,
                permissions: action.permissions
            };
        default:
            return state;
    }
}
