import { SET_PERSON } from "../Constraints/actionTypes";

export const setPerson = {
    type: SET_PERSON
}

export interface setPersonAction {
    personGuid: string;
    fullName: string;
}

export const setPersonWrapper = (dispatch: any, action: setPersonAction) => dispatch({ ...setPerson, ...action });
