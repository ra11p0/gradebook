import { APP_LOAD, LOG_IN, LOG_OUT, REFRESH_TOKEN } from '../../Constraints/actionTypes'

const defaultState = {
    appLoaded: false,
    isLoggedIn: false
};

export default (state: any = defaultState, action: any)=>{
    switch(action.type){
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
                    refreshToken: action.refreshToken
                }
            };
        case LOG_OUT:
            return {
                ...state,
                isLoggedIn: false,
                session: null
            };
        case REFRESH_TOKEN:
            return {
                ...state,
                isLoggedIn: true,
                session: {
                    ...state.session,
                    token: action.token,
                    refreshToken: action.refreshToken
                }
            };
        default:
            return state;
    }
}
