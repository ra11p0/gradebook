import { APP_LOAD, LOG_IN, LOG_OUT } from '../../Constraints/actionTypes'

const defaultState = {
    appLoaded: false,
    isLoggedIn: false
};

export default (state = defaultState, action: any)=>{
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
                    username: action.username,
                    token: action.token
                }
            };
        case LOG_OUT:
            return {
                ...state,
                isLoggedIn: false,
                session: null
            };
        default:
            return state;
    }
}
