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
                isLoggedIn: action.isLoggedIn,
            };
        case LOG_OUT:
            return {
                ...state,
                isLoggedIn: action.isLoggedIn,
            };
        default:
            return state;
    }
}
