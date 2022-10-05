import { combineReducers } from 'redux';
import { APP_LOAD } from '../Constraints/actionTypes';


const defaultState = {
    appLoaded: false,
};

const reducer = (state: any = defaultState, action: any) => {
    switch (action.type) {
        case APP_LOAD:
            return {
                ...state,
                appLoaded: action.isAppLoaded,
            };
        default:
            return state;
    }
}

export default combineReducers({
    common: reducer
});
