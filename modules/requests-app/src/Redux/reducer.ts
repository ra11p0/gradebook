import { combineReducers } from 'redux';
import { SET_CURRENTLY_EDITED_FIELD, SET_FIELDS } from '../Constraints/actionTypes';


const defaultState = {
    fields: [],
    currentlyEdited: ""
};

const reducer = (state: any = defaultState, action: any) => {
    switch (action.type) {
        case SET_FIELDS:
            return { ...state, fields: action.payload };
        case SET_CURRENTLY_EDITED_FIELD:
            return { ...state, currentlyEdited: action.payload };
        default:
            return state;
    }
}

export default combineReducers({
    common: reducer
});
