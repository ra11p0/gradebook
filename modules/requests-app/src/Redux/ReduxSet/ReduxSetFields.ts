import { SET_FIELDS } from "../../Constraints/actionTypes";
import Field from "../../Interfaces/Common/Field";
import { store } from "../store";

export default (fields: Field[]) => {
    store.dispatch({ type: SET_FIELDS, payload: fields });
};