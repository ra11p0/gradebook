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
                    refreshToken: action.refreshToken,
                    username: action.username,
                    userId: action.userId,
                    roles: action.roles
                }
            };
        case LOG_OUT:
            localStorage.removeItem('access_token');
            localStorage.removeItem('refresh');
            return {
                ...state,
                isLoggedIn: false,
                session: null
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
        default:
            return state;
    }
}
