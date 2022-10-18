import { SET_ANSWERS } from "../../Constraints/actionTypes";
import Field from "../../Interfaces/Common/Field";
import { store } from "../store";
import ReduxFixFields from "./ReduxFixFields";
import ReduxSetAnswer from "./ReduxSetAnswer";

export default (fields: Field[]) => {
    /*store.dispatch({
        type: SET_ANSWERS, payload: fields.map(field => ({ ...field, onBlur: undefined }))
    })*/
    fields.forEach(field => ReduxSetAnswer(field))
};