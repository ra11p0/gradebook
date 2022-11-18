import { APP_LOAD, LOG_IN, LOG_OUT, SET_LANGUAGE, SET_PERMISSIONS, SET_PERSON, SET_SCHOOL, SET_SCHOOLS_LIST, SET_USER } from '../../Constraints/accountActionTypes'
import ActionType from '../ActionTypes/accountActionTypes'

export type State = {
    appLoaded: boolean;
    school?: {
        schoolGuid: any;
        schoolName: any;
    }
    user?: {
        userId: any;
    }
    person?: {
        personGuid: any;
    }
    session?: {
        accessToken: any;
        refreshToken: any;
    }
    schoolsList?: [];
    permissions?: any;
    language?: any;
}

const defaultState = {
    appLoaded: false,
};

export default (state: State = defaultState, action: { type: ActionType, payload: any }) => {
    switch (action.type) {
        case ActionType.AppLoad:
            return {
                ...state,
                appLoaded: action.payload.isAppLoaded,
            };
        case ActionType.SetUser:
            return {
                ...state,
                user: {
                    userId: action.payload.userId,
                }
            }
        case ActionType.SetPerson:
            return {
                ...state,
                person: {
                    personGuid: action.payload.personGuid,
                }
            }
        case ActionType.LogIn:
            return {
                ...state,
                session: {
                    accessToken: action.payload.accessToken,
                    refreshToken: action.payload.refreshToken,
                }
            };
        case ActionType.LogOut:
            return {
                ...state,
                session: null,
                school: null,
                schoolsList: null,
                user: null
            };
        case ActionType.SetSchool:
            return {
                ...state,
                school: {
                    ...state.school,
                    schoolGuid: action.payload.schoolGuid,
                    schoolName: action.payload.schoolName
                }
            };
        case ActionType.SetSchoolsList:
            return {
                ...state,
                schoolsList: action.payload.schoolsList
            };
        case ActionType.SetPermissions:
            return {
                ...state,
                permissions: action.payload.permissions
            };
        case ActionType.SetLanguage:
            return {
                ...state,
                language: action.payload.language
            }
        default:
            return state;
    }
}
