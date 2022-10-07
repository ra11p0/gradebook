import { SET_CURRENTLY_EDITED_FIELD } from "../../Constraints/actionTypes";
import { store } from "../store";
import ReduxFixFields from "./ReduxFixFields";

export default (uuid: string) => {
    store.dispatch({ type: SET_CURRENTLY_EDITED_FIELD, payload: uuid });
    if (uuid === '') ReduxFixFields();
};